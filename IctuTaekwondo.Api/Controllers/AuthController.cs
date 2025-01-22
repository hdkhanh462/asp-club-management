using System.Security.Claims;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Schemas.Auth;
using System.Net;

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
        public async Task<ActionResult> Register(RegisterAdminSchema schema)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(schema);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Đăng ký tài khoản thành công." });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginSchema schema)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _authService.LoginAsync(schema);
            if (response == null) return Unauthorized();

            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserFullDetailResponse>> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var profile = await _authService.ProfileAsync(email);
            if (profile == null) return NotFound();

            return Ok(profile);
        }
    }
}
