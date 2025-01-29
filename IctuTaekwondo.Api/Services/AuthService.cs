using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Responses.Auth;
using Microsoft.EntityFrameworkCore;
using IctuTaekwondo.Api.Data;

namespace IctuTaekwondo.Shared.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema);
        public Task<JwtResponse?> LoginAsync(LoginSchema schema);
        public Task<UserFullDetailResponse?> GetProfileAsync(string userId);
        //public JwtResponse GenerateJwt(List<Claim> claims, DateTime? expires, string? algorithm);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ApiDbContext _context;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, ApiDbContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema)
        {
            var newUser = new User
            {
                AvatarUrl = schema.AvatarUrl,
                FullName = schema.FullName,
                Email = schema.Email,
                UserName = schema.Email
            };

            var createUserResult = await _userManager.CreateAsync(newUser, schema.Password);
            if (!createUserResult.Succeeded) return createUserResult;

            var addRoleResult = await _userManager.AddToRoleAsync(newUser, schema.Role.ToString());
            if (!addRoleResult.Succeeded) return addRoleResult;

            _context.Add(new UserProfile
            {
                User = newUser,
                Gender = schema.Gender,
                DateOfBirth = schema.DateOfBirth,
                Address = schema.Address,
                CurrentRank = schema.CurrentRank,
                JoinDate = schema.JoinDate
            });

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult <= 0)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "SaveProfileFailed",
                    Description = "Lưu thông tin người dùng thất bại"
                });
            }

            return IdentityResult.Success;
        }

        public async Task<JwtResponse?> LoginAsync(LoginSchema schema)
        {
            var user = await _userManager.FindByEmailAsync(schema.Email);
            if (user == null) return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, schema.Password);
            if (!isPasswordValid) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.FullName),
                    new(ClaimTypes.Email, user.Email!),
                    new(ClaimTypes.Role, string.Join(",", roles))
                };

            return GenerateJwt(claims);
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(string userId)
        {
            var user = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var userDetail = user.ToUserFullDetailResponse();
            userDetail.Roles = roles.ToList();

            return userDetail;
        }

        public JwtResponse GenerateJwt(List<Claim> claims, DateTime? expires = null, string? algorithm = SecurityAlgorithms.HmacSha256)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                claims: claims,
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                expires: expires ?? DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );

            return new JwtResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredAt = token.ValidTo
            };
        }
    }
}
