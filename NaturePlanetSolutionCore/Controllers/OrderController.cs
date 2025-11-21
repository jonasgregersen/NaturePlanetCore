using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DALOrder = DataAccessLayer.Model.Order;
using DTOOrder = DataTransferLayer.Model.OrderDto;

namespace NaturePlanetSolutionCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private OrderBLL _orderBLL;
        private DatabaseContext _context;

        public OrderController(UserManager<ApplicationUser> userManager, OrderBLL orderBLL,  DatabaseContext databaseContext)
        {
            _userManager = userManager;
            _orderBLL = orderBLL;
            _context = databaseContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(IFormCollection collection)
        {
            var cart = HttpContext.Session.GetObject<Cart>("cart");
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email");
                return View("Error");
            }

            // Create DTO
            var dtoOrder = new DTOOrder
            {
                Products = cart.Products,
                OrderNumber = cart.GenerateOrderNumber(),
                UserId = user.Id,
                OrderDate = DateTime.Now
            };
    
            // Add the order
            _orderBLL.CreateOrder(dtoOrder);
            HttpContext.Session.Remove("cart");
            return View("Confirmation", cart);

        }
        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
