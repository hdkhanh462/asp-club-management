using System.Net;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Schemas.Achievement;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/achievements")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        // GET: api/achievements?page=1&size=10
        // Lấy danh sách thành tích với phân trang
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAchievements(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var achievements = await _achievementService.GetAllAsync(page, size);

            return Ok(new ApiResponse<Paginator<AchievementResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = achievements
            });
        }

        // GET: api/achievements?page=1&size=10&name=abc&userId=abc123&eventId=123&start=2025-01-01&end=2025-01-31
        // Lọc danh sách thành tích với phân trang
        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetAchievementsWithFilter(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? name = null,
            [FromQuery] string? userId = null,
            [FromQuery] int? eventId = null,
            [FromQuery] DateOnly? start = null,
            [FromQuery] DateOnly? end = null)
        {
            if (end <= start) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Ngày bắt đầu phải lớn hơn Ngày kết thúc"
            });

            var achievements = await _achievementService
                .GetAllWithFilterAsync(page, size, name, userId, eventId, start, end);

            return Ok(new ApiResponse<Paginator<AchievementResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = achievements
            });
        }

        // GET: api/achievements/5
        // Lấy thông tin chi tiết thành tích theo id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAchievement(int id)
        {
            var achievement = await _achievementService.FindByIdAsync(id);
            if (achievement == null) return NotFound(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Thành tích không tồn tại"
            });

            return Ok(new ApiResponse<AchievementResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = achievement
            });
        }

        // PUT: api/achievements/5
        // Cập nhật thông tin thành tích
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PutAchievement(int id, [FromBody] AchievementUpdateSchema schema)
        {
            if (id != schema.Id) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest
            });

            try
            {
                var result = await _achievementService.UpdateAsync(id, schema);
                if (result == null) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cập nhật thành tích thất bại"
                });

                return Ok(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Cập nhật thành tích thành công",
                    Data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
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


        }

        // POST: api/achievements
        // Tạo mới thành tích
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PostAchievement([FromBody] AchievementCreateSchema schema)
        {
            try
            {
                var result = await _achievementService.CreateAsync(schema);
                if (result == null) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Tạo thành tích thất bại"
                });

                return Ok(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Tạo thành tích thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }
        }

        // DELETE: api/achievements/5
        // Xoá thành tích theo id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            try
            {
                var result = await _achievementService.DeleteAsync(id);
                if (!result) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Xoá thành tích thất bại"
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
                Message = "Xoá thành tích thành công"
            });
        }
    }
}
