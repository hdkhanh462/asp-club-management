using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController( UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var users = await _userManager.Users
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return Ok(users.Select(u => u.ToUserResponse()).ToList());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserFullDetailResponse>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var userDetail = user.ToUserFullDetailResponse();
            userDetail.Roles = roles;

            return Ok(user.ToUserResponse());
        }
    }
}
