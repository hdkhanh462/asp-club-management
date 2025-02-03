using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Responses;
using System.Net;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Controllers.Api
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/events?page=1&size=10
        // Lấy danh sách sự kiện với phân trang
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEvents(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var events = await _eventService.GetAllAsync(page, size);

            return Ok(new ApiResponse<PaginationResponse<EventResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = events
            });
        }
        
        // GET: api/events/filter?page=1&size=10&name=abc&status=1
        // Lọc danh sách sự kiện với phân trang
        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetEventsWithFilter(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10, 
            [FromQuery] string? name = null,
            [FromQuery] EventStatus? status = null)
        {
            var events = await _eventService.GetAllWithFilterAsync(page, size, name, status);

            return Ok(new ApiResponse<PaginationResponse<EventResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = events
            });
        }

        // GET: api/events/5
        // Lấy thông tin chi tiết sự kiện theo id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _eventService.GetByIdAsync(id);
            if (@event == null) return NotFound(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Sự kiện không tồn tại"
            });

            return Ok(new ApiResponse<EventFullDetailResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = @event
            });
        }

        // PUT: api/events/5
        // Cập nhật thông tin sự kiện
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PutEvent(int id, [FromBody] EventUpdateSchema schema)
        {
            if (id != schema.Id) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest
            });

            try
            {
                var result = await _eventService.UpdateAsync(id, schema);
                if (!result) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cập nhật sự kiện thất bại"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = ex.Message
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Cập nhật sự kiện thành công"
            });
        }

        // POST: api/events
        // Tạo mới sự kiện
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PostEvent([FromBody] EventCreateSchema schema)
        {
            var result = await _eventService.CreateAsync(schema);
            if (!result) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Tạo sự kiện thất bại"
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Tạo sự kiện thành công"
            });
        }

        // DELETE: api/events/5
        // Xoá sự kiện theo id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var result = await _eventService.DeleteAsync(id);
                if (!result) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Xoá sự kiện thất bại"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = ex.Message
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Xoá sự kiện thành công"
            });
        }
    }
}
