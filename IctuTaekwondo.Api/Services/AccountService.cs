using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IctuTaekwondo.Shared.Schemas.Account;

namespace IctuTaekwondo.Api.Services
{
    public interface IAccountService
    {
        public Task<UserResponse?> GetUserAsync(string userId);
        public Task<UserFullDetailResponse?> GetProfileAsync(string userId);
        public Task<IdentityResult> UpdateProfileAsync(string userId, UserUpdateSchema schema);
        public Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordSchema schema);
    }

    public class AccountService : IAccountService
    {
        private readonly ILogger<IAccountService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApiDbContext _context;
        private readonly IUploadFileService _fileService;

        public AccountService(ILogger<IAccountService> logger, UserManager<User> userManager, ApiDbContext context, IUploadFileService fileService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
        }

        public async Task<UserResponse?> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", userId);
                return null;
            }
            return user.ToUserResponse();
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(string userId)
        {
            var user = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError("User not found: {0}", userId);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDetail = user.ToUserFullDetailResponse();
            userDetail.Roles = roles.ToList();

            return userDetail;
        }

        public async Task<IdentityResult> UpdateProfileAsync(string userId, UserUpdateSchema schema)
        {
            var user = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", userId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            if (schema.Avatar != null)
            {
                var avatarUrl = user.AvatarUrl?.Split("static/")[1];

                try
                {
                    string fileName = await _fileService.SaveFileAsync(schema.Avatar);
                    user.AvatarUrl = fileName;

                    if (avatarUrl != null && avatarUrl != string.Empty) _fileService.DeleteFile(avatarUrl);
                }
                catch (FileNotFoundException)
                {
                    _logger.LogError($"DeleteFileFailed: {avatarUrl}");

                    //return IdentityResult.Failed(new IdentityError
                    //{
                    //    Code = "DeleteFileFailed",
                    //    Description = ex.Message
                    //});
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


            user.FullName = schema.FullName;
            user.PhoneNumber = schema.PhoneNumber;

            if (user.UserProfile == null)
            {
                user.UserProfile = new UserProfile();
                user.UserProfile.UserId = userId;
                _context.UserProfiles.Add(user.UserProfile);
                _context.SaveChanges();
            }

            user.UserProfile.Gender = schema.Gender;
            user.UserProfile.DateOfBirth = schema.DateOfBirth;
            user.UserProfile.CurrentRank = schema.CurrentRank;
            user.UserProfile.Address = schema.Address;
            user.UserProfile.JoinDate = schema.JoinDate;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordSchema schema)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", userId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            return await _userManager.ChangePasswordAsync(user, schema.CurrentPassword, schema.NewPassword);
        }
    }
}
