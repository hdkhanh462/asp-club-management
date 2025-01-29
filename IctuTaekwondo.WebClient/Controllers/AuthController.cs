using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiHelper _apiHelper;

        public AuthController(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
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

            var response = await _apiHelper.PostAsync<JwtResponse>("api/auth/login", model);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.Message != null) ModelState.AddModelError(string.Empty, response.Message);
                if (response.Errors != null)
                {
                    foreach (var (key, value) in response.Errors)
                    {
                        foreach (var error in value)
                        {
                            ModelState.AddModelError(key, error);
                        }
                    }
                }
                return View(model);
            }

            var jwtResponse = response.Data;

            if (jwtResponse == null)
            {
                ModelState.AddModelError(string.Empty, "Không thể đăng nhập vào hệ thống");
                return View(model);
            }

            if (Request.Cookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                Response.Cookies.Delete(GlobalConst.CookieAuthTokenKey);
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,
                Expires = model.RememberMe ? jwtResponse.ExpiredAt : null,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append(GlobalConst.CookieAuthTokenKey, jwtResponse.Token, cookieOptions);

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
            RegisterViewModel model,
            [FromQuery] string? next)
        {
            if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
