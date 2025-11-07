using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
