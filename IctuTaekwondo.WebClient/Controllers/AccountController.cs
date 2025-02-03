using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IctuTaekwondo.WebClient.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var userDetail = _accountService.GetProfile(Request.Cookies);
            if (userDetail == null)
            {
                _authService.Logout(Request.Cookies, Response.Cookies);
                return RedirectToAction("Login", "Auth");
            }

            return View(userDetail);
        }

        [Authorize]
        public IActionResult UpdateProfile()
        {
            var userDetail = _accountService.GetProfile(Request.Cookies);
            if (userDetail == null)
            {
                _authService.Logout(Request.Cookies, Response.Cookies);
                return RedirectToAction("Login", "Auth");
            }

            return View(new UserUpdateSchema
            {
                AvatarUrl = userDetail.AvatarUrl,
                FullName = userDetail.FullName,
                PhoneNumber = userDetail.PhoneNumber,
                Gender = userDetail.Profile.Gender,
                DateOfBirth = userDetail.Profile.DateOfBirth,
                CurrentRank = userDetail.Profile.CurrentRank,
                Address = userDetail.Profile.Address,
                JoinDate = userDetail.Profile.JoinDate
            });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateSchema schema)
        {
            if (!ModelState.IsValid) return View(schema);

            var response = await _accountService.UpdateProfileAsync(schema, ModelState, Request.Cookies);
            if (!response) return View(schema);

            TempData["SuccessMessage"] = "Cập nhật thành công!";

            return RedirectToAction("UpdateProfile", "Account");
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordSchema schema)
        {
            if (!ModelState.IsValid) return View(schema);

            var response = await _accountService.ChangePasswordAsync(schema, ModelState, Request.Cookies);
            if (!response) return View(schema);

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";

            return RedirectToAction("ChangePassword", "Account");
        }
    }
}
