using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IctuTaekwondo.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Api.Data;

namespace IctuTaekwondo.Api.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema);
        public Task<JwtResponse?> LoginAsync(LoginSchema schema);
    }

    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ApiDbContext _context;
        private readonly IUploadFileService _fileService;

        public AuthService(ILogger<AuthService> logger,
            IConfiguration configuration,
            UserManager<User> userManager,
            ApiDbContext context,
            IUploadFileService fileService)
        {
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterAdminSchema schema)
        {
            var newUser = new User
            {
                FullName = schema.FullName,
                Email = schema.Email,
                PhoneNumber = schema.PhoneNumber,
                UserName = schema.Email,
                CreatedAt = DateTime.UtcNow,
            };

            if (schema.Avatar != null)
            {
                try
                {
                    var avatarUrl = await _fileService.SaveFileAsync(schema.Avatar);
                    newUser.AvatarUrl = avatarUrl;

                }
                catch (ArgumentException ex)
                {
                    _logger.LogError("InvalidFileExtension");

                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "InvalidFileExtension",
                        Description = ex.Message
                    });
                }
            }

            var createUserResult = await _userManager.CreateAsync(newUser, schema.Password);
            if (!createUserResult.Succeeded)
            {
                _logger.LogError("Create user failed: {0}", createUserResult.Errors);
                return createUserResult;
            }
            var rolesString = schema.Roles.Select(r => r.ToString());
            var addRoleResult = await _userManager.AddToRolesAsync(newUser, rolesString);
            if (!addRoleResult.Succeeded)
            {
                _logger.LogError("Add role failed: {0}", addRoleResult.Errors);
                return addRoleResult;
            };

            _context.Add(new UserProfile
            {
                User = newUser,
            });

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult <= 0)
            {
                _logger.LogError("Save profile failed");
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
            if (user == null)
            {
                _logger.LogError("User not found: {0}", schema.Email);
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, schema.Password);
            if (!isPasswordValid)
            {
                _logger.LogError("Invalid password: {0}", schema.Email);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Email, user.Email!),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return GenerateJwt(claims);
        }

        public JwtResponse GenerateJwt(List<Claim> claims, 
            DateTime? expires = null, 
            string algorithm = SecurityAlgorithms.HmacSha256)
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
