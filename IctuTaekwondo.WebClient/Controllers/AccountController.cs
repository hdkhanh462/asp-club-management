using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IctuTaekwondo.WebClient.Services;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AccountController(IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userDetail = _accountService.GetProfileAsync(Request.Cookies);
            if (userDetail == null)
            {
                _authService.Logout(Request.Cookies, Response.Cookies);
                return RedirectToAction("Login", "Auth");
            }

            return View(userDetail);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Profile(UserProfileSchema schema)
        {
            var response = await _accountService.UpdateProfileAsync(schema, ModelState, Request.Cookies);
            if (!response) return View(schema);

            return RedirectToAction("Profile", "Account");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordSchema schema)
        {
            var response = await _accountService.ChangePasswordAsync(schema, ModelState, Request.Cookies);
            if (!response) return View(schema);

            return RedirectToAction("Profile", "Account");
        }
    }
}
