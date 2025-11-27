using Business.Extensions;
using Business.Model;
using Business.Services;
using DataAccessLayer.Model;
using DataTransferLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private CartService _cartService;

    public CartViewComponent(CartService cartService)
    {
        _cartService = cartService;
    }
    public IViewComponentResult Invoke()
    {
        var cart = _cartService.GetCart();
        if (cart == null)
        {
            cart = new Cart();
            cart.Products = new List<ProductDto>();
        }

        return View(cart);
    }
}