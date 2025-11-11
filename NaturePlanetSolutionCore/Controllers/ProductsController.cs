using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
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
            .Select(p => p.Name)
            .ToList();
        return View(products);
    } 
    [HttpGet("Products/{category}")]
    public IActionResult Index(string category)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category)
            .Select(p => p.Name)
            .ToList();
        return View(products);
    }
    [HttpGet("Products/{category1}/{category2}")]
    public IActionResult Index(string category1, string category2)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category1 && p.Product_Category_2 == category2)
            .Select(p => p.Name)
            .ToList();
        return View(products);
    }
    [HttpGet("Products/{category1}/{category2}/{category3}")]
    public IActionResult Index(string category1, string category2, string category3)
    {
        var products = _productBLL.getAllProducts()
            .Where(p => p.Product_Category_1 == category1 &&  p.Product_Category_2 == category2 && p.Product_Category_3 == category3)
            .Select(p => p.Name)
            .ToList();
        return View(products);
    }
}