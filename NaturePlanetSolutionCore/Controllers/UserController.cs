using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NaturePlanetSolutionCore.Models.ViewModels;

namespace NaturePlanetSolutionCore.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly DatabaseContext _context;

        private readonly OrderBLL _orderBLL;
        
        private readonly ProductBLL _productBLL;
        

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, DatabaseContext context, OrderBLL orderBLL, ProductBLL productBLL)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _orderBLL = orderBLL;
            _productBLL = productBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {

            }

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
                FullName = viewModel.FullName


            };

            var createUser = await _userManager.CreateAsync(user, viewModel.Password);

            if (createUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in createUser.Errors)
            {
                Console.WriteLine(error);
                ModelState.AddModelError("", error.Description);
            }

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

            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            var result = await _signInManager.PasswordSignInAsync(
                viewModel.Email,
                viewModel.Password,
                isPersistent: viewModel.RememberMe,
                lockoutOnFailure: false
                );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Incorrect email or password.");

            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Cart()
        {
            var order = HttpContext.Session.GetObject<Cart>("cart") ?? new Cart();
            return View("Cart", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(string productId)
        {
            var cart = HttpContext.Session.GetObject<Cart>("cart") ?? new Cart();
            var productToRemove = cart.Products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                cart.Products.Remove(productToRemove);
                HttpContext.Session.SetObject("cart", cart);
            }
            return RedirectToAction("Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var orders = _orderBLL.GetUserOrders(user.Id);
            return View(orders);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var viewModel = new ProfileViewModel
            {
                Email = user.Email,
                Password = user.PasswordHash
            };
            return View("ProfilePage", viewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "Your password has been changed.";
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("UserList");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SetUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            // Remove from both roles first
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (await _userManager.IsInRoleAsync(user, "User"))
                await _userManager.RemoveFromRoleAsync(user, "User");

            // Add to selected role
            if (!string.IsNullOrEmpty(role))
                await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> RemoveProduct(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new Exception("Der skete en fejl med at fjerne produktet.");
            }

            var cart = HttpContext.Session.GetObject<Cart>("cart") ?? new Cart();
            var product = await _productBLL.GetProductByName(productName);
            
            cart.RemoveProduct(product);
            HttpContext.Session.SetObject("cart", cart);
            return View("Cart", cart);
        }
    }
}
