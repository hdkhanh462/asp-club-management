using System.Security.Claims;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Services;
using IctuTaekwondo.Shared.Responses.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.Shared.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventRegisterationsController : ControllerBase
    {
        private readonly IEventRegisterationService _service;

        public EventRegisterationsController(
            IEventRegisterationService eventRegisterationService)
        {
            _service = eventRegisterationService;
        }

        // POST : api/events/5/register
        // Đăng ký tham gia sự kiện
        [HttpPost("{id}/register")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<EventFullDetailResponse>> RegisterEvent(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var rerulst = await _service.RegisterEvent(id, userId, true);

            if (rerulst != EventRegisterationResult.Success)
            {
                return BadRequest(new { Message = rerulst.GetStatusText("Bạn") });
            }

            return Ok(new { Message = "Đăng ký sự kiện thành công." });
        }

        // POST : api/events/5/unregister
        // Hủy đăng ký tham gia sự kiện
        [HttpPost("{id}/unregister")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<EventFullDetailResponse>> UnregisterEvent(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.UnregisterEvent(id, userId);

            if (result != EventRegisterationResult.Success)
            {
                return BadRequest(new { Message = result.GetStatusText("Bạn") });
            }

            return Ok(new { Message = "Huỷ đăng ký tham gia sự kiện thành công." });
        }

        // GET : api/events/5/registrations
        // Lấy danh sách người đăng ký tham gia sự kiện
        [HttpGet("{id}/registrations")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<IEnumerable<UserResgiteredEventResponse>>> GetEventRegistrations(
            int id,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var @event = await _service.GetEventWithRegisterationAsync(id);
            if (@event == null) return NotFound();

            var registrations = @event.EventRegistrations
                .Skip((page - 1) * size)
                .Take(size)
                .Select(er =>
                {
                    var user = er.User.ToUserResgiteredEventResponse();
                    user.RegisteredAt = er.CreatedAt;
                    return user;
                })
                .ToList();

            return Ok(registrations);
        }

        // GET : api/events/5/registrations/user123
        // Lấy thông tin đăng ký tham gia sự kiện của người dùng
        [HttpGet("{id}/registrations/{userId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<UserResgiteredEventResponse>> GetEventRegistration(
            int id,
            string userId)
        {
            var @event = await _service.GetEventWithRegisterationAsync(id);
            if (@event == null) return NotFound();

            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration == null) return NotFound();

            var registeredUser = registration.User.ToUserResgiteredEventResponse();
            registeredUser.RegisteredAt = registration.CreatedAt;

            return Ok(registeredUser);
        }

        // POST : api/events/5/registrations
        // Thêm người dùng vào sự kiện
        [HttpPost("{id}/registrations")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<EventFullDetailResponse>> AddUserToEvent(
            int id,
            [FromQuery] string userId)
        {
            var result = await _service.RegisterEvent(id, userId);
            if (result != EventRegisterationResult.Success)
            {
                return BadRequest(new { Message = result.GetStatusText() });
            }

            return Ok(new { Message = "Thêm người dùng vào sự kiện thành công." });
        }

        // DELETE : api/events/5/registrations/user123
        // Xóa người dùng khỏi sự kiện
        [HttpDelete("{id}/registrations/{userId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<EventFullDetailResponse>> RemoveUserFromEvent(
            int id,
            string userId)
        {
            var result = await _service.UnregisterEvent(id, userId);
            if (result != EventRegisterationResult.Success)
            {
                return BadRequest(new { Message = result.GetStatusText() });
            }

            return Ok(new { Message = "Xóa người dùng khỏi sự kiện thành công." });
        }
    }
}
