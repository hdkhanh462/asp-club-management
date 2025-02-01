using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Schemas.Account;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> UserDetail()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Không tìm thấy thông tin người dùng",
            });
            var userDetail = await _accountService.GetUserAsync(userId);
            if (userDetail == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không tìm thấy thông tin người dùng",
                });
            }

            return Ok(new ApiResponse<UserResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = userDetail
            });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Không tìm thấy thông tin người dùng",
            });

            var userDetail = await _accountService.GetProfileAsync(userId);
            if (userDetail == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không tìm thấy thông tin người dùng",
                });
            }

            return Ok(new ApiResponse<UserFullDetailResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = userDetail
            });
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateSchema schema)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Không tìm thấy thông tin người dùng",
            });

            var result = await _accountService.UpdateProfileAsync(userId, schema);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cập nhật thông tin không thành công",
                    Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                    ),
                });
            }
            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Cập nhật thông tin thành công",
            });
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordSchema schema)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Không tìm thấy thông tin người dùng",
            });

            var result = await _accountService.ChangePasswordAsync(userId, schema);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Thay đổi mật khẩu không thành công",
                    Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                    ),
                });
            }
            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Thay đổi mật khẩu thành công",
            });
        }
    }
}
