using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace NaturePlanetSolutionCore.Controllers;

public class ProductsController : Controller
{
    private ProductBLL _productBLL;

    public ProductsController(ProductBLL productBLL)
    {
        _productBLL = productBLL;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productBLL.getAllProducts();
        return View(products);
    }

    [HttpGet("Products/{category1?}/{category2?}/{category3?}")]
    public async Task<IActionResult> FilterByCategory(string? category1 = null, string? category2 = null,
        string? category3 = null)
    {
        var queryProducts = await _productBLL.getAllProductsByCategory(category1, category2, category3);
        return View("index", queryProducts);
    }

    [HttpGet("Products/Details/{productName}")]
    public async Task<IActionResult> Details(string productName)
    {
        var product = await _productBLL.GetProductByName(productName);
        Console.WriteLine(product.Name);
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

        var products = await _productBLL.SearchProducts(query);
        return View("index", products);
    }

    public async Task<IActionResult> AddToOrder(string productName)
    {
        if (productName.IsNullOrEmpty())
        {
            throw new Exception("Der skete en fejl med at tilføje produktet.");
        }

        var cart = HttpContext.Session.GetObject<Cart>("cart") ?? new Cart();
        var product = await _productBLL.GetProductByName(productName);
        
        cart.AddProduct(product);
        HttpContext.Session.SetObject("cart", cart);
        return View("Details", product);
    }
}