using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NaturePlanetSolutionCore.Models.ViewModels;

namespace NaturePlanetSolutionCore.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); 
            }

            var user = new ApplicationUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                
            };

            var createUser = await _userManager.CreateAsync(user, viewModel.Password);

            return View(viewModel);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            

            return View(viewModel);
        }
    }
}
