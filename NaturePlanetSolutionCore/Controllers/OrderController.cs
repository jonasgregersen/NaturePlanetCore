using Business.Model;
using DataAccessLayer.Mappers;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DALOrder = DataTransferLayer.Model.OrderDto;
using DTOOrder = DataTransferLayer.Model.OrderDto;

namespace NaturePlanetSolutionCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private OrderBLL _orderBLL;

        public OrderController(UserManager<ApplicationUser> userManager, OrderBLL orderBLL)
        {
            _userManager = userManager;
            _orderBLL = orderBLL;
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
            var navn = collection["name-order"];
            var email = collection["email-order"];

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email");
            }
            var cart = HttpContext.Session.GetObject<Cart>("cart") ?? new Cart();

            var dtoOrder = new DALOrder(
                orderNumber: cart.GenerateOrderNumber(),
                products: cart.Products
            );
            
            var dalOrder = OrderMapper.Map(dtoOrder);
            user.Orders.Add(dalOrder);
            _orderBLL.CreateOrder(dtoOrder);
            return View("Index");
        }
    }
}
