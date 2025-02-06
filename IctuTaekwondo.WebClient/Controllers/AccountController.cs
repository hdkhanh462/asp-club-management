using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IctuTaekwondo.WebClient.Services;
using IctuTaekwondo.WebClient.Models;
using Htmx;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userDetail = _accountService.GetProfile(Request.Cookies);
            if (userDetail == null) return Unauthorized();

            return View(new UpdateUserViewModel
            {
                Id = userDetail.Id,
                Email = userDetail.Email,
                PhoneNumber = userDetail.PhoneNumber,
                Roles = userDetail.Roles,
                AvatarUrl = userDetail.AvatarUrl,
                FullName = userDetail.FullName,
                Gender = userDetail.Profile.Gender,
                DateOfBirth = userDetail.Profile.DateOfBirth,
                Address = userDetail.Profile.Address,
                CurrentRank = userDetail.Profile.CurrentRank,
                JoinDate = userDetail.Profile.JoinDate,
            });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDetail = await _accountService.UpdateProfileAsync(model, ModelState, Request.Cookies);
                if (userDetail != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật thành công",
                    }));
                    return PartialView("Users/_UpdateUserFormPartial", new UpdateUserViewModel
                    {
                        Id = userDetail.Id,
                        Email = userDetail.Email,
                        Roles = userDetail.Roles,
                        PhoneNumber = userDetail.PhoneNumber,
                        AvatarUrl = userDetail.AvatarUrl,
                        FullName = userDetail.FullName,
                        Gender = userDetail.Profile.Gender,
                        DateOfBirth = userDetail.Profile.DateOfBirth,
                        Address = userDetail.Profile.Address,
                        CurrentRank = userDetail.Profile.CurrentRank,
                        JoinDate = userDetail.Profile.JoinDate,
                    });
                }
            }
            return PartialView("Users/_UpdateUserFormPartial", model);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordSchema schema)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePasswordAsync(schema, ModelState, Request.Cookies);
                if (response)
                {
                    Response.Htmx(h => h.WithTrigger(
                    "add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Đổi mật khẩu thành công",
                    }));
                }
            }

            return PartialView("_ChangePasswordFormPartial", schema);
        }
    }
}
