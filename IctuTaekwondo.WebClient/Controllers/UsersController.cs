using System.Security.Claims;
using Htmx;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.WebClient.Models;
using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        private readonly string[] allowedRedirectUrl =
        [
            "/users",
        ];

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(
            [FromQuery] List<string> search,
            [FromQuery] List<string> order,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var paginator = await _userService.GetAllAsync(
                page, size,
                search, order,
                ModelState, Request.Cookies);

            ViewData["QueryParameters"] = Request.Query;
            return View(paginator);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(string id)
        {
            var userDetail = await _userService.GetFullDetailAsync(id, ModelState, Request.Cookies);
            if (userDetail == null) return NotFound();

            var curentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userDetail.Id == curentUserId) return RedirectToAction("Profile", "Account");

            var roles = userDetail.Roles
                 .Select(roleString => Enum.TryParse(roleString, out Role role) ? (Role?)role : null)
                 .Where(role => role.HasValue)
                 .Select(role => role.Value)
                 .ToList();

            return View(new UpdateUserViewModel
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterViewModel model)
        {
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var isSuccess = await _authService.RegisterAsync(model,
                    ModelState,
                    Request.Cookies,
                    Response.Cookies);
                if (isSuccess)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Đăng ký thành công",
                    }));
                    return PartialView("_RegisterUserFormPartial", new RegisterViewModel());
                }
            }
            return PartialView("_RegisterUserFormPartial", model);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, [FromForm] UpdateUserViewModel model)
        {
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var userDetail = await _userService.UpdateAsync(id, model, ModelState, Request.Cookies);
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
                        AvatarUrl = userDetail.AvatarUrl,
                        PhoneNumber = userDetail.PhoneNumber,
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

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id, [FromQuery] string? next)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var result = await _userService.DeleteAsnyc(id, ModelState, Request.Cookies);
            await Task.Delay(TimeSpan.FromSeconds(3));

            if (ModelState[string.Empty] == null)
            {
                Response.Htmx(h =>
                {
                    if (!string.IsNullOrEmpty(next) && allowedRedirectUrl.Contains(next.ToLower()))
                        h.WithTrigger("add-sweetalert2-toast", new
                        {
                            icon = "success",
                            title = "Xoá người dùng thành công",
                            redirectUrl = next
                        });
                    else
                        h.WithTrigger("add-sweetalert2-toast", new
                        {
                            icon = "success",
                            title = "Xoá người dùng thành công",
                        })
                        .WithTrigger("delete-elm", $"Row-{id}");
                });

                return NoContent();
            }

            Response.Htmx(h =>
            {
                h.WithTrigger("add-sweetalert2-toast", new
                {
                    icon = "error",
                    title = "Xoá người dùng thất bại",
                    errors = ModelState[string.Empty]!.Errors.Select(e => e.ErrorMessage).ToList()
                });
            });

            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetPassword(string id, [FromForm] SetPasswordViewModel model)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null) return Unauthorized();

            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.SetPasswordAsync(
                    currentUserId, id,
                    model,
                    ModelState,
                    Request.Cookies);
                if (isSuccess)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Đặt mật khẩu thành công",
                    })
                    .WithTrigger("close-modal", "Modal-Set-Password"));
                }
            }

            return PartialView("_SetPasswordFormPartial", model);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoles(string id, [FromForm] UpdateRolesViewModel model)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null) return Unauthorized();

            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.UpdateRolesAsync(
                    currentUserId, id,
                    model,
                    ModelState,
                    Request.Cookies);
                if (isSuccess)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật vai trò thành công",
                    })
                    .WithTrigger("close-modal", "Modal-Update-Roles"));
                }
            }

            return PartialView("_UpdateRolesFormPartial", model);
        }
    }
}
