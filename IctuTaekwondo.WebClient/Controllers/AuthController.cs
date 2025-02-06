using IctuTaekwondo.WebClient.Models;
using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly string[] allowedRedirectUrl =
        [
            "/accounts",
            "/auth/register",
        ];

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.LoginAsync(model, 
                ModelState, 
                Request.Cookies, 
                Response.Cookies);

            if (!result) return View(model);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(
            [FromForm] RegisterViewModel model,
            [FromQuery] string? next)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.RegisterAsync(model,
                ModelState,
                Request.Cookies,
                Response.Cookies);

            if (!result) return View(model);

            if (!string.IsNullOrEmpty(next) && allowedRedirectUrl.Contains(next.ToLower())) 
                return Redirect(next);

            ModelState.Clear();

            TempData["SuccessMessage"] = "Đăng ký thành công!";

            return RedirectToAction("Register", "Auth");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _authService.Logout(Request.Cookies, Response.Cookies);

            return RedirectToAction("Index", "Home");
        }
    }
}
