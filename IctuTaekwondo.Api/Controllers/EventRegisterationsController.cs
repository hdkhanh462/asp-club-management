using System.Net;
using System.Security.Claims;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.Api.Controllers
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
        public async Task<IActionResult> RegisterEvent(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.RegisterAsync(id, userId, true);

            if (result != EventRegisterationResult.Success) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = result.GetStatusText("Bạn")
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Đăng ký tham gia sự kiện thành công"
            });
        }

        // POST : api/events/5/unregister
        // Hủy đăng ký tham gia sự kiện
        [HttpDelete("{id}/unregister")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> UnregisterEvent(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.UnregisterAsync(id, userId);

            if (result != EventRegisterationResult.Success) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = result.GetStatusText("Bạn")
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Huỷ đăng ký tham gia sự kiện thành công"
            });
        }

        // GET : api/events/5/registrations?page=1&size=10
        // Lấy danh sách người đăng ký tham gia sự kiện với phân trang
        [HttpGet("{id}/registerations")]
        [Authorize]
        public async Task<IActionResult> GetAll(
            int id,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var @event = await _service.GetFullDetailAsync(id);
            if (@event == null) return NotFound(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Sự kiện không tồn tại"
            });

            var paginator = _service.GetAllRegisteration(@event, page, size);

            return Ok(new ApiResponse<PaginationResponse<EventResgiteredUsersResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = paginator
            });
        }

        // POST : api/events/5/registrations?userId=user123
        // Thêm người dùng vào sự kiện
        [HttpPost("{id}/registrations")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddUserToEvent(
            int id,
            [FromQuery] string userId)
        {
            var result = await _service.RegisterAsync(id, userId);
            if (result != EventRegisterationResult.Success) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = result.GetStatusText()
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Thêm người dùng vào sự kiện thành công"
            });
        }

        // DELETE : api/events/5/registrations/user123
        // Xóa người dùng khỏi sự kiện
        [HttpDelete("{id}/registrations/{userId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> RemoveUserFromEvent(
            int id,
            string userId)
        {
            var result = await _service.UnregisterAsync(id, userId);
            if (result != EventRegisterationResult.Success) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = result.GetStatusText()
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Xoá người dùng khỏi sự kiện thành công"
            });
        }
    }
}
