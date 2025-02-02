using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Responses;
using System.Net;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Api.Services;

namespace IctuTaekwondo.api.Controllers
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
        public async Task<IActionResult> Register([FromForm] RegisterAdminSchema schema)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Đăng ký không thành công",
            });

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
            try
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
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                });
            }
        }
    }
}
