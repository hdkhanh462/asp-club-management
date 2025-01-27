using IctuTaekwondo.Shared.Data;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly SharedDbContext _context;
        private readonly UserManager<User> _userManager;

        public AccountController(SharedDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var model = user.ToUserFullDetailResponse();
            model.Roles = await _userManager.GetRolesAsync(user);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(UserProfileSchema schema)
        {
            if (!ModelState.IsValid) return View(schema);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var existingProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (existingProfile == null)
            {
                var newProfile = new UserProfile
                {
                    UserId = user.Id,
                    Gender = schema.Gender,
                    DateOfBirth = schema.DateOfBirth,
                    Address = schema.Address,
                    CurrentRank = schema.CurrentRank,
                    JoinDate = schema.JoinDate,
                };

                _context.Add(newProfile);
            }
            else
            {
                existingProfile.Gender = schema.Gender;
                existingProfile.DateOfBirth = schema.DateOfBirth;
                existingProfile.Address = schema.Address;
                existingProfile.CurrentRank = schema.CurrentRank;
                existingProfile.JoinDate = schema.JoinDate;

                _context.Update(existingProfile);
            }

            await _context.SaveChangesAsync();

            return View(existingProfile);
        }
    }
}
