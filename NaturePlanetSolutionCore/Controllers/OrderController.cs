using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
