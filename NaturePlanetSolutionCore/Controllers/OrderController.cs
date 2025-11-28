using Business.Model;
using Business.Services;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DTOOrder = DataTransferLayer.Model.OrderDto;

namespace NaturePlanetSolutionCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private OrderBLL _orderBLL;
        private CartService _cartService;
        private ILogger<OrderController> _logger;

        public OrderController(UserManager<ApplicationUser> userManager, OrderBLL orderBLL, CartService cartService, ILogger<OrderController> logger)
        {
            _userManager = userManager;
            _orderBLL = orderBLL;
            _cartService = cartService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            _logger.LogInformation("User checkout requested: {UserId}", _userManager.GetUserId(User));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(IFormCollection collection)
        {
            var cart = _cartService.GetCart();
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email");
                return View("Error");
            }

            var dtoOrder = new DTOOrder
            {
                Products = cart.Products,
                OrderNumber = cart.GenerateOrderNumber(),
                UserId = user.Id,
                OrderDate = DateTime.Now
            };
    
            _orderBLL.CreateOrder(dtoOrder);
            _logger.LogInformation("Order created successfully");
            _cartService.ClearCart();
            return View("Confirmation", cart);

        }
    }
}
