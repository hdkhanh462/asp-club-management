using System.Net;
using System.Security.Claims;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
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
            try
            {
                var result = await _userService.UpdateProfileAsync(id, schema);
                if (result == null) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cập nhật hồ sơ thất bại",
                });

                return Ok(new ApiResponse<UserFullDetailResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Cập nhật hồ sơ thành công",
                    Data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = ex.Message,
                });
            }
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
        public async Task<IActionResult> SetUserPassword(string id, [FromBody] AdminSetPasswordSchema schema)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
            });

            var result = await _userService.SetPasswordAsync(currentUserId, id, schema);
            if (!result.Succeeded) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Đặt mật khẩu thất bại",
                Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                )
            });

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Đặt mật khẩu thành công"
            });
        }

        [HttpPut("{id}/roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles(string id, [FromBody] UpdateRolesSchema schema)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var currentUser = await _userManager.FindByIdAsync(currentUserId);
                if (currentUser != null)
                {
                    var isCorrect = await _userManager.CheckPasswordAsync(currentUser, schema.YourPassword);
                    if (!isCorrect) return BadRequest(new ApiResponse<object>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new Dictionary<string, string[]>
                        {
                            {"YourPassword", ["Mật khẩu của bạn không chính xác"] }
                        }
                    });

                    var updateRolesResult = await _userService.UpdateRolesAsync(id, schema.Roles.Select(r=>r.ToString()).ToList());
                    if (!updateRolesResult.Succeeded)
                    {
                        return BadRequest(new ApiResponse<object>
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Cập nhật vai trò thất bại",
                            Errors = updateRolesResult.Errors.ToDictionary(
                                kvp => kvp.Code,
                                kvp => new[] { kvp.Description }
                            )
                        });
                    }

                    var targetUser = await _userService.GetProfileByIdAsync(id);
                    return Ok(new ApiResponse<UserFullDetailResponse>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Cập nhật vai trò thành công",
                        Data = targetUser
                    });
                }
            }

            return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
            });
        }
    }
}
