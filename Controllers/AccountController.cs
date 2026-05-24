using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET /Account/Login
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Business");

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // POST /Account/Login
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Business");
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(vm);
        }

        // GET /Account/Register
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Business");
            return View(new RegisterViewModel());
        }

        // POST /Account/Register
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new IdentityUser { UserName = vm.Email, Email = vm.Email };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                // Store the business name as a user claim so it persists
                await _userManager.AddClaimAsync(user, new Claim("BusinessName", vm.BusinessName));
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["Toast"] = "✓ Account created! Welcome.";
                return RedirectToAction("Index", "Business");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(vm);
        }

        // POST /Account/Logout
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
