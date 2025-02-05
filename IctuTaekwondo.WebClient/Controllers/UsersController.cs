using Htmx;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.WebClient.Models;
using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

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

        public IActionResult Detail(int id)
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var result = await _userService.DeleteAsnyc(id, ModelState, Request.Cookies);
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
                    h.WithTrigger("init-dismisses",
                        new { toDismissId = toast.GetHtmlId() },
                        timing: HtmxTriggerTiming.AfterSwap)
                    .WithTrigger("delete-elm", $"row-{id}", timing: HtmxTriggerTiming.AfterSwap);
                });

                return PartialView("Partials/_ToastMessagePartial", toast);
            }

            toast.Type = ToastType.Error;
            toast.Message = "Xoá người dùng thất bại";
            toast.Errors = ModelState[string.Empty]!.Errors;
            Response.Htmx(h =>
            {
                h.WithTrigger("init-dismisses",
                        new { toDismissId = toast.GetHtmlId() },
                        timing: HtmxTriggerTiming.AfterSwap);
            });

            return PartialView("Partials/_ToastMessagePartial", toast);
        }
    }
}
