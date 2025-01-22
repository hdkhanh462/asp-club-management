using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Responses.Auth;

namespace IctuTaekwondo.Shared.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema);
        public Task<LoginResponse?> LoginAsync(LoginSchema schema);
        public Task<UserFullDetailResponse?> ProfileAsync(string? email);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var newUser = new User
            {
                AvatarUrl = schema.AvatarUrl,
                FullName = schema.FullName,
                Email = schema.Email,
                UserName = schema.Email
            };

            var result = await _userManager.CreateAsync(newUser, schema.Password);
            if (!result.Succeeded) return result;

            return await _userManager.AddToRoleAsync(newUser, schema.Role.ToString());
        }
        
        public async Task<LoginResponse?> LoginAsync(LoginSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

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

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    claims: claims,
                    audience: _configuration["Jwt:Audience"],
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(
                        key,
                        SecurityAlgorithms.HmacSha256
                    )
                );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredAt = token.ValidTo
            };
        }

        public async Task<UserFullDetailResponse?> ProfileAsync(string? email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var userDetail = user.ToUserFullDetailResponse();
            userDetail.Roles = roles.ToList();

            return userDetail;
        }
    }
}
