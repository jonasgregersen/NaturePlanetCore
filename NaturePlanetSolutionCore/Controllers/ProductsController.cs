using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.Controllers;

public class ProductsController : Controller
{
    private ProductBLL _productBLL;

    public ProductsController(ProductBLL productBLL)
    {
        _productBLL = productBLL;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var products = _productBLL.getAllProducts()
            .ToList();
        return View(products);
    }

    [HttpGet("Products/{category}")]
    public IActionResult Index(string category)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category)
            .ToList();
        return View(products);
    }

    [HttpGet("Products/{category1}/{category2}")]
    public IActionResult Index(string category1, string category2)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category1 && p.Product_Category_2 == category2)
            .ToList();
        return View(products);
    }

    [HttpGet("Products/{category1}/{category2}/{category3}")]
    public IActionResult Index(string category1, string category2, string category3)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category1 && p.Product_Category_2 == category2 &&
                        p.Product_Category_3 == category3)
            .ToList();
        return View(products);
    }

    [HttpGet("Products/Details/{productName}")]
    public IActionResult Details(string productName)
    {
        var product = _productBLL.GetProductByName(productName);
        Console.WriteLine(product.Name);
        return View("Details", product);
    }

    [HttpGet("Products/Search")]
    public IActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            ViewBag.Message = "Skriv et søgeord!";
            return View("Index", new List<Product>());
        }

        var products = _productBLL.SearchProducts(query);
        return View("index", products);
    }
}