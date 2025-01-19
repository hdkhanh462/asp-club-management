using System.Security.Claims;
using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventRegisterationsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<User> _userManager;

        public EventRegisterationsController(ApiDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST : api/events/5/register
        // Đăng ký tham gia sự kiện
        [HttpPost("{id}/register")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<EventFullDetailResponse>> RegisterEvent(int id)
        {
            var @event = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null) return NotFound();

            var errorMessage = IsEventValidToRegister(@event);
            if (errorMessage != null) return BadRequest(new { Message = errorMessage });

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration != null)
            {
                return BadRequest(new { Message = "Bạn đã đăng ký tham gia sự kiện này rồi." });
            }

            var newRegistration = new EventRegistration
            {
                EventId = id,
                UserId = userId,
            };
            _context.EventRegistrations.Add(newRegistration);

            var result = await _context.SaveChangesAsync();
            if (result == 0) return BadRequest();

            return Ok(new { Message = "Đăng ký sự kiện thành công." });
        }

        // POST : api/events/5/unregister
        // Hủy đăng ký tham gia sự kiện
        [HttpPost("{id}/unregister")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<EventFullDetailResponse>> UnregisterEvent(int id)
        {
            var @event = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);

            if (registration == null)
            {
                return BadRequest(new { Message = "Bạn chưa đăng ký tham gia sự kiện này." });
            }

            _context.EventRegistrations.Remove(registration);

            var result = await _context.SaveChangesAsync();
            if (result == 0) return BadRequest();

            return Ok(new { Message = "Huỷ đăng ký tham gia sự kiện thành công." });
        }

        private string? IsEventValidToRegister(Event @event)
        {
            if (@event.EndDate.HasValue && @event.EndDate.Value < DateTime.Now)
            {
                return "Sự kiện đã kết thúc.";
            }

            if (@event.StartDate < DateTime.Now)
            {
                return "Sự kiện đã diễn ra.";
            }

            if (@event.MaxParticipants.HasValue && @event.EventRegistrations.Count >= @event.MaxParticipants.Value)
            {
                return "Sự kiện đã đủ số lượng người tham gia.";
            }

            return null;
        }
    }
}
