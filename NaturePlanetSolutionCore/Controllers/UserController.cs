using Business.Model;
using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Business.Services;
using NaturePlanetSolutionCore.Models.ViewModels;

namespace NaturePlanetSolutionCore.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrderBLL _orderBLL;
        private readonly ProductBLL _productBLL;
        private readonly CartService _cartService;
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, UserRepository repository, OrderBLL orderBLL, ProductBLL productBLL, CartService cartService, ILogger<UserController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _orderBLL = orderBLL;
            _productBLL = productBLL;
            _cartService = cartService;
            _userRepository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserById(id);

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
                _logger.LogInformation("User registered: {Email}", viewModel.Email);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in createUser.Errors)
            {
                Console.WriteLine(error);
                _logger.LogError("Error registering user: {ErrorMessage}", error.Description);
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
                _logger.LogInformation("User logged in: {Email}", viewModel.Email);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Incorrect email or password.");

            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out: {Email}", _userManager.GetUserId(User));
            return RedirectToAction("Login", "User");
        }

        public IActionResult Cart()
        {
            var order = _cartService.GetCart();
            _logger.LogInformation("User cart requested: {UserId}", _userManager.GetUserId(User));
            return View("Cart", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(string productId)
        {
            var cart = _cartService.GetCart();
            var productToRemove = cart.Products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                cart.Products.Remove(productToRemove);
                _cartService.SaveCart(cart);
                _logger.LogInformation("Product removed from cart: {ProductId}", productId);
                return Ok(new { success = true });
            }
            return BadRequest(new { success = false });
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
            _logger.LogInformation("User orders requested: {UserId}", user.Id);
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
            _logger.LogInformation("User profile requested: {UserId}", user.Id);
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
                _logger.LogInformation("User password changed: {UserId}", user.Id);
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
            _logger.LogInformation("User list requested");
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
            _logger.LogInformation("User promoted to admin: {UserId}", user.Id);
            return RedirectToAction("UserList");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SetUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (await _userManager.IsInRoleAsync(user, "User"))
                await _userManager.RemoveFromRoleAsync(user, "User");

            if (!string.IsNullOrEmpty(role))
                await _userManager.AddToRoleAsync(user, role);
            _logger.LogInformation("User role set: {UserId}, {Role}", user.Id, role);
            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> RemoveProduct(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new Exception("Der skete en fejl med at fjerne produktet.");
            }

            var cart = _cartService.GetCart();
            var product = await _productBLL.GetProductByName(productName);
            
            cart.RemoveProduct(product);
            _cartService.SaveCart(cart);
            _logger.LogInformation("Product removed from cart: {ProductName}", productName);
            return View("Cart", cart);
        }
    }
}
