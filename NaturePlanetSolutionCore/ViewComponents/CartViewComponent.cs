using DataAccessLayer.Model;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetObject<OrderDto>("order");
        if (cart == null)
        {
            cart = new OrderDto();
            cart.Products = new List<ProductDto>();
        }

        return View(cart);
    }
}