using System.Security.Claims;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly string[] nextUrlsValid = ["/dashboard", "/users"];

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác");
                return View(model);
            }

            var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!isPasswordValid.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác");
                return View(model);
            }

            var claims = new List<Claim>
                {
                    new("FullName", user.FullName),
                    new("AvatarUrl", user.AvatarUrl ?? string.Empty)
                };

            await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(
            RegisterViewModel model,
            [FromQuery] string? next)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, model.Role.ToString());

            if (!string.IsNullOrEmpty(next) && nextUrlsValid.Contains(next)) return Redirect(next);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
