using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IctuTaekwondo.Shared.Data;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Schemas.Event;
using Microsoft.AspNetCore.Identity;
using IctuTaekwondo.Shared.Enums;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace IctuTaekwondo.Shared.Controllers.Api
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public EventsController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/events
        // Lấy danh sách sự kiện với phân trang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetEvents(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var events = await _context.Events
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return events.Select(e => e.ToEventResponse()).ToList();
        }

        // GET: api/events/5
        // Lấy thông tin chi tiết sự kiện theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<EventFullDetailResponse>> GetEvent(int id)
        {
            var @event = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null) return NotFound();

            return @event.ToEventFullDetailResponse();
        }

        // PUT: api/events/5
        // Cập nhật thông tin sự kiện
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PutEvent(int id, EventUpdateSchema updateSchema)
        {
            if (id != updateSchema.Id) return BadRequest();

            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            @event.Name = updateSchema.Name;
            @event.StartDate = DateTime.SpecifyKind(updateSchema.StartDate, DateTimeKind.Unspecified);
            @event.EndDate = updateSchema.EndDate.HasValue ? DateTime.SpecifyKind(updateSchema.EndDate.Value, DateTimeKind.Unspecified) : (DateTime?)null;
            @event.Location = updateSchema.Location;
            @event.Fee = updateSchema.Fee;
            @event.MaxParticipants = updateSchema.MaxParticipants;
            @event.Description = updateSchema.Description;

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id)) return NotFound();
                else throw;
            }

            return Ok(new {Message = "Cập nhật sự kiện thành công." });
        }

        // POST: api/events
        // Tạo mới sự kiện
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<EventFullDetailResponse>> PostEvent(EventCreateSchema createSchema)
        {
            var @event = new Event
            {
                Name = createSchema.Name,
                StartDate = DateTime.SpecifyKind(createSchema.StartDate, DateTimeKind.Unspecified),
                EndDate = createSchema.EndDate.HasValue ? DateTime.SpecifyKind(createSchema.EndDate.Value, DateTimeKind.Unspecified) : (DateTime?)null,
                Location = createSchema.Location,
                Fee = createSchema.Fee,
                MaxParticipants = createSchema.MaxParticipants,
                Description = createSchema.Description,
            };

            _context.Events.Add(@event);
            var result = await _context.SaveChangesAsync();

            if (result == 0) return BadRequest();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event.ToEventFullDetailResponse());
        }

        // DELETE: api/events/5
        // Xoá sự kiện theo id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            _context.Events.Remove(@event);

            var result = await _context.SaveChangesAsync();
            if (result == 0) return BadRequest();

            return Ok(new { Message = "Xoá sự kiện thành công." });
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
