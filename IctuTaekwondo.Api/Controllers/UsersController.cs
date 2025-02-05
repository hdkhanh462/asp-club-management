using System.Net;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var paginator = await _userService.GetAllAsync(page, size);
            return Ok(new ApiResponse<PaginationResponse<UserResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = paginator
            });
        }
        
        [HttpGet("filter")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersWithFilter(
            [FromQuery] List<string> search,
            [FromQuery] List<string> order,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var paginator = await _userService.GetAllWithFilterAsync(page, size, search, order);
            return Ok(new ApiResponse<PaginationResponse<UserResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = paginator
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetProfileByIdAsync(id);
            if (user == null) return NotFound(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
            });

            return Ok(new ApiResponse<UserFullDetailResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = user
            });
        }

        [HttpPut("{id}/profile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromForm] UserUpdateSchema schema)
        {
            var result = await _userService.UpdateProfileAsync(id, schema);

            if (!result.Succeeded) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Cập nhật hồ sơ thất bại",
                Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                )
            });

            return Ok(new ApiResponse<UserResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Cập nhật hồ sơ thành công"
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.Succeeded) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Xoá người dùng thất bại",
                Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                )
            });

            return Ok(new ApiResponse<UserResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Xoá người dùng thành công"
            });
        }

        [HttpPut("{id}/set-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetUserPassword(string id, [FromBody] SetPasswordSchema schema)
        {
            var result = await _userService.SetPasswordAsync(id, schema);

            if (!result.Succeeded) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Đặt mật khẩu thất bại",
                Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                )
            });

            return Ok(new ApiResponse<UserResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Đặt mật khẩu thành công"
            });
        }
    }
}
