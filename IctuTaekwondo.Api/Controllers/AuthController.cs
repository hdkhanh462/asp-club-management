using System.Security.Claims;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Responses;
using System.Net;
using IctuTaekwondo.Shared.Responses.Auth;

namespace IctuTaekwondo.Shared.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterAdminSchema schema)
        {
            var result = await _authService.RegisterAsync(schema);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Đăng ký không thành công",
                    Errors = result.Errors.ToDictionary(
                        kvp => kvp.Code,
                        kvp => new[] { kvp.Description }
                    ),
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Đăng ký thành công",
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginSchema schema)
        {
            var result = await _authService.LoginAsync(schema);
            if (result == null)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Email hoặc mật khẩu không chính xác",
                });
            }

            return Ok(new ApiResponse<JwtResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Đăng nhập thành công",
                Data = result
            });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Không tìm thấy thông tin người dùng",
            });

            var userDetail = await _authService.GetProfileAsync(userId);
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
    }
}
