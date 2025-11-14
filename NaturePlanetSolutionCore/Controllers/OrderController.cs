using Business.Model;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DALOrder = DataTransferLayer.Model.Order;
using DTOOrder = DataTransferLayer.Model.Order;

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
            user.Orders.Add(order);
            _orderBLL.CreateOrder(order);
            return View("Index");
        }
    }
}
