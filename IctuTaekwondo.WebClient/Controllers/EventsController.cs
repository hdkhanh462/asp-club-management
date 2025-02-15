using System.Security.Claims;
using Htmx;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventService;
        private readonly IRegisterationService _registerationService;
        private readonly IAccountService _accountService;
        private readonly List<string> validUrls = ["/Events"];

        public EventsController(IEventsService eventService, IRegisterationService registerationService, IAccountService accountService)
        {
            _eventService = eventService;
            _registerationService = registerationService;
            _accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();

            var events = await _eventService.GetAllAsync(token,page, size);

            ViewData["CurrentUser"] = currentUser;

            return View(events);
        }

        [Authorize]
        public async Task<IActionResult> Detail(int id, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty((userId)))
            {
                ViewData["QueryParameters"] = Request.Query;

                var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

                var currentEvent = await _eventService.FindByIdAsync(token,id);
                if (currentEvent == null) return NotFound();

                var currentUser = await _accountService.GetProfileAsync(token);
                if (currentUser == null) return Unauthorized();

                var currentEventRegisterations = await _registerationService.GetAllAsync(token,id, page, size);

                ViewData["CurrentUser"] = currentUser;
                ViewData["CurrentEvent"] = currentEvent;
                ViewData["RegisterationPaginator"] = currentEventRegisterations;
                ViewData["IsCurrentUserRegistered"] = currentEvent.Status.Contains(EventStatus.Registered);

                return View(new EventUpdateSchema
                {
                    Id = currentEvent.Id,
                    Name = currentEvent.Name,
                    Location = currentEvent.Location,
                    StartDate = currentEvent.StartDate,
                    EndDate = currentEvent.EndDate,
                    Fee = currentEvent.Fee,
                    MaxParticipants = currentEvent.MaxParticipants,
                    Description = currentEvent.Description
                });
            }

            return Unauthorized();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(int id, EventUpdateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

                var currentUser = await _accountService.GetProfileAsync(token);
                if (currentUser == null) return Unauthorized();

                ViewData["CurrentUser"] = currentUser;

                var newEvent = await _eventService.UpdateAsync(token,id, schema);
                if (newEvent != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật sự kiện thành công",
                    }));

                    return PartialView("_UpdateEventFormPartial", new EventUpdateSchema
                    {
                        Id = newEvent.Id,
                        Name = newEvent.Name,
                        Location = newEvent.Location,
                        StartDate = newEvent.StartDate,
                        EndDate = newEvent.EndDate,
                        Fee = newEvent.Fee,
                        MaxParticipants = newEvent.MaxParticipants,
                        Description = newEvent.Description
                    });
                }
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Cập nhật sự kiện không thành công",
            }));

            return PartialView("_UpdateEventFormPartial", schema);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string? next)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var isSuccess = await _eventService.DeleteAsync(token, id);
                if (isSuccess)
                {
                    object? toast = toast = new
                    {
                        icon = "success",
                        title = "Xoá sự kiện thành công",
                    };

                    if (!string.IsNullOrEmpty(next) && validUrls.Contains(next))
                        toast = new
                        {
                            icon = "success",
                            title = "Xoá sự kiện thành công",
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
                title = "Xoá sự kiện không thành công",
            }));

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([FromForm] EventCreateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var newUser = await _eventService.CreateAsync(token,schema);
                if (newUser != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Tạo sự kiện thành công",
                    }));
                    return PartialView("_CreateEventFormPartial", new EventCreateSchema());
                }
            }
            return PartialView("_CreateEventFormPartial", schema);
        }

        [HttpDelete]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Unregister(int id)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var currentEvent = await _eventService.FindByIdAsync(token,id);
            if (currentEvent == null) return NotFound();

            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();

            ViewData["CurrentUser"] = currentUser;

            var isSuccess = await _registerationService.UnregisterAsync(token,id);
            if (isSuccess)
            {
                ViewData["IsRegistered"] = false;

                Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                {
                    icon = "success",
                    title = "Huỷ đăng ký tham gia sự kiện thành công",
                }));
                return PartialView("_RegisterationButtonsPartial", currentEvent);
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Huỷ đăng ký tham gia sự kiện không thành công",
            }));

            return PartialView("_RegisterationButtonsPartial", currentEvent);
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Register(int id)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var currentEvent = await _eventService.FindByIdAsync(token,id);
            if (currentEvent == null) return NotFound();

            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();

            ViewData["CurrentUser"] = currentUser;

            var error = await _registerationService.RegisterAsync(token,id);
            if (string.IsNullOrEmpty(error))
            {
                ViewData["IsRegistered"] = true;

                Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                {
                    icon = "success",
                    title = "Đăng ký tham gia sự kiện thành công",
                }));

                return PartialView("_RegisterationButtonsPartial", currentEvent);
            }

            var errorMsg = string.Empty;
            if (ModelState.TryGetValue(string.Empty, out var msg))
            {
                errorMsg = msg.Errors.FirstOrDefault()?.ErrorMessage;
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = $"Đăng ký tham gia sự kiện không thành công.<br>{errorMsg}",
            }));

            return PartialView("_RegisterationButtonsPartial", currentEvent);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ManagerUnregister(int id, string userId)
        {
            if (!Request.IsHtmx()) return BadRequest();
            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var currentEvent = await _eventService.FindByIdAsync(token,id);
            if (currentEvent == null) return NotFound();

            var isSuccess = await _registerationService.ManagerUnregisterAsync(token,id, userId);
            if (isSuccess)
            {
                ViewData["IsRegistered"] = false;

                Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                {
                    icon = "success",
                    title = "Huỷ đăng ký tham gia sự kiện cho người dùng thành công",
                }));
                return NoContent();
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Huỷ đăng ký tham gia sự kiện cho người dùng không thành công",
            }));

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ManagerRegister(int id)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var currentEvent = await _eventService.FindByIdAsync(token, id);
            if (currentEvent == null) return NotFound();

            var currentUser = await _accountService.GetProfileAsync(token);
            if (currentUser == null) return Unauthorized();

            ViewData["CurrentUser"] = currentUser;

            var error = await _registerationService.RegisterAsync(token,id);
            if (string.IsNullOrEmpty(error))
            {
                ViewData["IsRegistered"] = true;

                Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                {
                    icon = "success",
                    title = "Đăng ký tham gia sự kiện thành công",
                }));

                return PartialView("_RegisterationButtonsPartial", currentEvent);
            }

            var errorMsg = string.Empty;
            if (ModelState.TryGetValue(string.Empty, out var msg))
            {
                errorMsg = msg.Errors.FirstOrDefault()?.ErrorMessage;
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = $"Đăng ký tham gia sự kiện không thành công.<br>{errorMsg}",
            }));

            return PartialView("_RegisterationButtonsPartial", currentEvent);
        }
    }
}
