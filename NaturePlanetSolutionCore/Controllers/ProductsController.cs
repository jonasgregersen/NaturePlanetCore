using Business.Model;
using Business.Services;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace NaturePlanetSolutionCore.Controllers;

public class ProductsController : Controller
{
    private ProductBLL _productBLL;
    private CartService _cartService;
    private ILogger<ProductsController> _logger;

    public ProductsController(ProductBLL productBLL, CartService cartService, ILogger<ProductsController> logger)
    {
        _productBLL = productBLL;
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productBLL.getAllProductsAsync();
        return View(products);
    }

    [HttpGet("Products/{category1}/{category2?}/{category3?}")]
    public async Task<IActionResult> FilterByCategory(string category1, string? category2 = null,
        string? category3 = null)
    {
        var queryProducts = await _productBLL.GetAllProductsByCategoryAsync(category1, category2, category3);
        var route = new string[] {category1, category2, category3};
        ViewBag.Route = route;
        _logger.LogInformation("Products filtered by category: {Category1}, {Category2}, {Category3}", category1, category2, category3);
        return View("index", queryProducts);
    }

    [HttpGet("Products/Details/{productName}")]
    public async Task<IActionResult> Details(string productName)
    {
        var product = await _productBLL.GetProductByName(productName);
        _logger.LogInformation("Product details requested: {ProductName}", productName);
        return View("Details", product);
    }

    [HttpGet("Products/Search")]
    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            ViewBag.Message = "Skriv et søgeord!";
            return View("Index", new List<ProductDto>());
        }
        try
        {
            var products = await _productBLL.SearchProductsAsync(query);
            ViewBag.Query = query;
            _logger.LogInformation("Products searched for: {Query}", query);
            return View("index", products);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            _logger.LogError("Error searching products: {ErrorMessage}", e.Message);
            return View("Index", new List<ProductDto>());
        }
    }

    public async Task<IActionResult> AddToOrder(string productName)
    {
        if (productName.IsNullOrEmpty())
        {
            throw new Exception("Der skete en fejl med at tilføje produktet.");
        }

        var cart = _cartService.GetCart();
        var product = await _productBLL.GetProductByName(productName);
        
        cart.AddProduct(product);
        _cartService.SaveCart(cart);
        _logger.LogInformation("Product added to cart: {ProductName}", productName);
        return View("Details", product);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Products/CreateProduct")]
    public async Task<IActionResult> CreateProduct()
    {
        FillViewBagCategories();
        return View("CreateProduct");
    }
    
    [HttpPost("Products/CreateProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductDto productDto)
    {
        if (productDto.Id == null)
        {
            ModelState.AddModelError("", "Produktet skal tilføres en ID.");
        }

        if (productDto.Name == null)
        {
            ModelState.AddModelError("", "Produktet skal have et navn.");
        }

        if (productDto.EAN == null)
        {
            ModelState.AddModelError("", "Prouktet skal have et EAN.");
        }

        if (productDto.ErpSource == null)
        {
            ModelState.AddModelError("", "Prouktet skal have en ERP source.");
        }

        if (productDto.QuantityInBag <= 0 || productDto.QuantityInBag == null)
        {
            ModelState.AddModelError("", "Prouktet skal have antal i bag.");
        }
        
        if (productDto.Weight <= 0)
        {
            ModelState.AddModelError("", "Prouktet skal have vægt.");
        }

        if (productDto.Segment == null)
        {
            ModelState.AddModelError("", "Prouktet skal have et segment.");
        }
        // Hvis kategori 1 mangler, men kategori 2 eller 3 er udfyldt
        if (string.IsNullOrWhiteSpace(productDto.Product_Category_1) &&
            (!string.IsNullOrWhiteSpace(productDto.Product_Category_2) ||
             !string.IsNullOrWhiteSpace(productDto.Product_Category_3)))
        {
            ModelState.AddModelError("Product_Category_1",
                "Du skal vælge kategori 1 før du kan vælge kategori 2 eller 3.");
            return View(productDto);
        }

        // Hvis kategori 2 mangler, men kategori 3 er udfyldt
        if (string.IsNullOrWhiteSpace(productDto.Product_Category_2) &&
            !string.IsNullOrWhiteSpace(productDto.Product_Category_3))
        {
            ModelState.AddModelError("Product_Category_2",
                "Du skal vælge kategori 2 før du kan vælge kategori 3.");
            return Redirect("CreateProduct");
        }


        if (!ModelState.IsValid)
        {
            FillViewBagCategories();
            return View(productDto);
        }

        try
        {
            await _productBLL.CreateProduct(productDto);
            _logger.LogInformation("Product created: {ProductName}", productDto.Name);
        }
        catch (Exception e)
        {
            FillViewBagCategories();
            ModelState.AddModelError("", e.Message);
            return View(productDto);
        }
        return RedirectToAction("Index");
    }

    private async Task FillViewBagCategories()
    {
        var products = await _productBLL.getAllProductsAsync();
        var categories1 = products
            .Where(c => c.Product_Category_1 != null || c.Product_Category_1 != "")
            .Select(c => c.Product_Category_1)
            .Distinct()
            .ToList();
        var categories2 = products
            .Where(c => c.Product_Category_2 != null || c.Product_Category_2 != "")
            .Select(c => c.Product_Category_2)
            .Distinct()
            .ToList();
        var categories3 = products
            .Where(c => c.Product_Category_3 != null || c.Product_Category_3 != "")
            .Select(c => c.Product_Category_3)
            .Distinct()
            .ToList();
        ViewBag.Categories1 = categories1;
        ViewBag.Categories2 = categories2;
        ViewBag.Categories3 = categories3;
    }
    
}