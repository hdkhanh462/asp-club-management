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
            ViewData["Id"] = id;

            var user = await _userService.GetFullDetailAsync(id, ModelState, Request.Cookies);
            if (user == null) return NotFound();

            return View(new UserUpdateSchema
            {
                AvatarUrl = user.AvatarUrl,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Profile.Gender,
                DateOfBirth = user.Profile.DateOfBirth,
                Address = user.Profile.Address,
                CurrentRank = user.Profile.CurrentRank,
                JoinDate = user.Profile.JoinDate,
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(string id, UserUpdateSchema schema)
        {
            ViewData["Id"] = id;
            if (!ModelState.IsValid) return View(schema);

            var isSucsess = await _userService.UpdateAsync(id, schema, ModelState, Request.Cookies);
            if (!isSucsess) return View(schema);

            TempData["SuccessMessage"] = "Cập nhật thành công";

            return RedirectToAction("Detail");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, UserUpdateSchema schema)
        {
            @ViewData["Id"] = id;
            if (!ModelState.IsValid) return View(schema);

            var isSucsess = await _userService.UpdateAsync(id, schema, ModelState, Request.Cookies);
            if (!isSucsess) return View(schema);

            TempData["SuccessMessage"] = "Cập nhật thành công";

            return RedirectToAction("Detail");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            string id,
            [FromQuery] string? next)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var result = await _userService.DeleteAsnyc(id, ModelState, Request.Cookies);
            await Task.Delay(TimeSpan.FromSeconds(15));
            var toast = new ToastMessageModelView
            {
                Id = Guid.NewGuid().ToString(),
            };

            if (ModelState[string.Empty] == null)
            {
                toast.Type = ToastType.Success;
                toast.Message = "Xoá người dùng thành công";
                Response.Htmx(h =>
                {
                    if (!string.IsNullOrEmpty(next) && allowedRedirectUrl.Contains(next.ToLower()))
                        h.WithTrigger("add-toast", new
                        {
                            toastId = toast.GetHtmlId(),
                            toastLifeTime = 3000,
                            redirectUrl = next,
                        },
                        timing: HtmxTriggerTiming.AfterSwap);
                    else
                        h.WithTrigger("add-toast", new
                        {
                            toastId = toast.GetHtmlId(),
                        },
                        timing: HtmxTriggerTiming.AfterSwap)
                        .WithTrigger("delete-elm", $"Row-{id}", timing: HtmxTriggerTiming.AfterSwap);
                });

                return PartialView("Partials/_ToastMessagePartial", toast);
            }

            toast.Type = ToastType.Error;
            toast.Message = "Xoá người dùng thất bại";
            toast.Errors = ModelState[string.Empty]!.Errors;
            Response.Htmx(h =>
            {
                h.WithTrigger("add-toast", new
                {
                    toastId = toast.GetHtmlId()
                },
                timing: HtmxTriggerTiming.AfterSwap);
            });

            return PartialView("Partials/_ToastMessagePartial", toast);
        }
    }
}
