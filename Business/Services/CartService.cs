using Business.Constants;
using Business.Extensions;
using Business.Model;
using Microsoft.AspNetCore.Http;

namespace Business.Services;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }
    
    public Cart GetCart()
    {
        return _httpContextAccessor.HttpContext.Session
            .GetObject<Cart>(SessionKeys.CartKey) ?? new Cart();
    }

    public void SaveCart(Cart cart)
    {
        if (cart == null)
        {
            throw new ArgumentNullException(nameof(cart));
        }
        
        _httpContextAccessor.HttpContext.Session
            .SetObject(SessionKeys.CartKey, cart);
    }

    public void ClearCart()
    {
        _httpContextAccessor.HttpContext.Session
            .Remove(SessionKeys.CartKey);
    }
}