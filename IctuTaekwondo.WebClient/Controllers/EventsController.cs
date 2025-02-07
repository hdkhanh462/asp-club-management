using System.Drawing;
using Htmx;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.WebClient.Models;
using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var events = await _eventService.GetAllAsync(page, size, ModelState, Request);
            return View(events);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] EventCreateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            if (ModelState.IsValid)
            {
                var newUser = await _eventService.CreateAsync(schema, ModelState, Request);
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
    }
}
