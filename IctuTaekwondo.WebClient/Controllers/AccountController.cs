using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IctuTaekwondo.WebClient.Models;
using Htmx;
using IctuTaekwondo.Shared.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Services.Account;

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
        public async Task<IActionResult> Profile()
        {
            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var userDetail = await _accountService.GetProfileAsync(token);
            if (userDetail == null) return Unauthorized();

            var roles = userDetail.Roles
                         .Select(roleString => Enum.TryParse(roleString, out Role role) ? (Role?)role : null)
                         .Where(role => role.HasValue)
                         .Select(role => role.Value)
                         .ToList();

            return View(new UpdateUserViewModel
            {
                Id = userDetail.Id,
                Email = userDetail.Email,
                PhoneNumber = userDetail.PhoneNumber,
                Roles = roles,
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
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
                var userDetail = await _accountService.UpdateProfileAsync(token, model);
                if (userDetail != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật thành công",
                    }));
                    var roles = userDetail.Roles
                         .Select(roleString => Enum.TryParse(roleString, out Role role) ? (Role?)role : null)
                         .Where(role => role.HasValue)
                         .Select(role => role.Value)
                         .ToList();

                    return PartialView("Users/_UpdateUserFormPartial", new UpdateUserViewModel
                    {
                        Id = userDetail.Id,
                        Email = userDetail.Email,
                        Roles = roles,
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
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
                var response = await _accountService.ChangePasswordAsync(token,schema);
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
