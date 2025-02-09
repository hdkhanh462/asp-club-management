using Htmx;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly IAchievementsService _service;
        private readonly IAccountService _accountService;
        private readonly List<string> validUrls = ["/Achievements"];

        public AchievementsController(IAchievementsService service, IAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();
            ViewData["CurrentUser"] = currentUser;

            var list = await _service.GetAllAsync(token, page, size);

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Detail(int id, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();
            ViewData["CurrentUser"] = currentUser;

            var detail = await _service.FindByIdAsync(token, id);
            if (detail == null) return NotFound();
            ViewData["CurrentAchievement"] = detail;

            return View(new AchievementUpdateSchema
            {
                Id = detail.Id,
                Name = detail.Name,
                DateAchieved = detail.DateAchieved,
                UserId = detail.User.Id,
                EventId = detail.Event?.Id,
                Description = detail.Description
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(int id, AchievementUpdateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var currentUser = await _accountService.GetProfileAsync(token);
                if (currentUser == null) return Unauthorized();

                ViewData["CurrentUser"] = currentUser;

                var @new = await _service.UpdateAsync(token, id, schema);
                if (@new != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật thành tích thành công",
                    }));

                    return PartialView("_UpdateAchievementFormPartial", new AchievementUpdateSchema
                    {
                        Id = @new.Id,
                        Name = @new.Name,
                        DateAchieved = @new.DateAchieved,
                        UserId = @new.User.Id,
                        EventId = @new.Event?.Id,
                        Description = @new.Description
                    });
                }
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Cập nhật thành tích không thành công",
            }));

            return PartialView("_UpdateAchievementFormPartial", schema);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string? next)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var isSuccess = await _service.DeleteAsync(token, id);
                if (isSuccess)
                {
                    object? toast = toast = new
                    {
                        icon = "success",
                        title = "Xoá thành tích thành công",
                    };

                    if (!string.IsNullOrEmpty(next) && validUrls.Contains(next))
                        toast = new
                        {
                            icon = "success",
                            title = "Xoá thành tích thành công",
                            redirectUrl = next
                        };

                    Response
                        .Htmx(h => h
                        .WithTrigger("add-sweetalert2-toast", toast)
                        .WithTrigger("delete-elm", $"Row-{id}"));

                    return NoContent();
                }
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Xoá thành tích không thành công",
            }));

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([FromForm] AchievementCreateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var newUser = await _service.CreateAsync(token,schema);
                if (newUser != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Tạo thành tích thành công",
                    }));
                    return PartialView("_CreateAchievementFormPartial", new AchievementCreateSchema());
                }
            }
            return PartialView("_CreateAchievementFormPartial", schema);
        }

    }
}
