using Business.Model;
using DataAccessLayer.Model;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetObject<Cart>("order");
        if (cart == null)
        {
            cart = new Cart();
            cart.Products = new List<ProductDto>();
        }

        return View(cart);
    }
}