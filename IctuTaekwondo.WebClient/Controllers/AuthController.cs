using System.Net;
using Htmx;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService apiService;
        private readonly string[] allowedRedirectUrl =
        [
            "/accounts",
            "/auth/register",
        ];

        public AuthController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginSchema schema)
        {
            if (!ModelState.IsValid) return View(schema);

            try
            {
                var response = await apiService.PostAsync<JwtResponse>("api/auth/login", schema.ToStringContent());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    foreach (var (key, value) in response.Errors)
                    {
                        var keyName = string.Empty;

                        if (key.Contains("Email") || key.Contains("UserName")) keyName = "Email";
                        if (key.Contains("Password")) keyName = "ConfirmPassword";

                        foreach (var error in value)
                        {
                            ModelState.AddModelError(keyName, error);
                        }
                    }
                }

                var jwt = response.Data;

                if (jwt == null)
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập không thành công.\nTài khoản hoặc mật khảu không chính xác");
                    return View(schema);
                }

                if (Request.Cookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
                {
                    Response.Cookies.Delete(GlobalConst.CookieAuthTokenKey);
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    //Secure = true,
                    Expires = schema.RememberMe ? jwt.ExpiredAt : null,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append(GlobalConst.CookieAuthTokenKey, jwt.Token, cookieOptions);

                return RedirectToAction("Profile", "Account");
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? string.Empty);
                return View(schema);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                Response.Cookies.Delete(GlobalConst.CookieAuthTokenKey);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
