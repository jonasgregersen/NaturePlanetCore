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
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(IFormCollection collection)
        {
            var navn = collection["name-order"];
            var email = collection["email-order"];

            var user = _userManager.FindByEmailAsync(email).Result;
            
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email");
            }
            var order = HttpContext.Session.GetObject<DTOOrder>("order");
            var dalOrder = OrderMapper.Map(order);
            user.Orders.Add(dalOrder);
            _orderBLL.CreateOrder(dtoOrder);
            return View("Confirmation");
        }
    }
}
