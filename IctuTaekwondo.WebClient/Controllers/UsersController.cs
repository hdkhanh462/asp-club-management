using System.Drawing;
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
        private readonly string[] allowedRedirectUrl =
        [
            "/users",
        ];

        public UsersController(IUserService userService)
        {
            _userService = userService;
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
            var user = await _userService.GetFullDetailAsync(id, ModelState, Request.Cookies);
            if (user == null) return NotFound();

            var curentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (user.Id == curentUserId) return RedirectToAction("Profile", "Account");

            return View(new AdminUserUpdateSchema
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                FullName = user.FullName,
                Gender = user.Profile.Gender,
                DateOfBirth = user.Profile.DateOfBirth,
                Address = user.Profile.Address,
                CurrentRank = user.Profile.CurrentRank,
                JoinDate = user.Profile.JoinDate,
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, AdminUserUpdateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            if (!ModelState.IsValid) return PartialView("_UpdateFormPartial", schema);

            var isSucsess = await _userService.UpdateAsync(id, schema, ModelState, Request.Cookies);
            if (!isSucsess) return PartialView("_UpdateFormPartial", schema);

            var userDetail = await _userService.GetFullDetailAsync(id, ModelState, Request.Cookies);
            if (userDetail == null) return PartialView("_UpdateFormPartial", schema);

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "success",
                title = "Cập nhật thành công",
            }));

            return PartialView("_UpdateFormPartial", new AdminUserUpdateSchema
            {
                Id = userDetail.Id,
                Email = userDetail.Email,
                PhoneNumber = userDetail.PhoneNumber,
                AvatarUrl = userDetail.AvatarUrl,
                FullName = userDetail.FullName,
                Gender = userDetail.Profile.Gender,
                DateOfBirth = userDetail.Profile.DateOfBirth,
                CurrentRank = userDetail.Profile.CurrentRank,
                Address = userDetail.Profile.Address,
                JoinDate = userDetail.Profile.JoinDate
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            string id,
            [FromQuery] string? next)
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
    }
}
