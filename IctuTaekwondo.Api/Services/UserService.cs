using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Services
{
    public interface IUserService
    {
        Task<UserFullDetailResponse?> UpdateProfileAsync(string id, UserUpdateSchema schema);
        Task<IdentityResult> DeleteAsync(string id);
        Task<UserResponse?> FindByIdAsync(string id);
        Task<UserFullDetailResponse?> GetProfileByIdAsync(string id);
        Task<PaginationResponse<UserResponse>> GetAllAsync(int page, int size);
        Task<IdentityResult> ChangePasswordAsync(string id, ChangePasswordSchema schema);
        Task<IdentityResult> SetPasswordAsync(string currentUserId, string userToSetId, AdminSetPasswordSchema schema);
        public Task<IdentityResult> UpdateRolesAsync(string targetTd, IEnumerable<string> newRoles);
        Task<PaginationResponse<UserResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            List<string> search,
            List<string> order);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUploadFileService _fileService;
        private readonly UserManager<User> _userManager;
        private readonly ApiDbContext _context;
        private readonly List<string> _orderable;

        public UserService(ILogger<UserService> logger,
            IUploadFileService fileService,
            UserManager<User> userManager,
            ApiDbContext context)
        {
            _logger = logger;
            _fileService = fileService;
            _userManager = userManager;
            _context = context;
            _orderable = new List<string>()
            {
                nameof(User.FullName),
                $"-{nameof(User.FullName)}",
                nameof(User.CreatedAt),
                $"-{nameof(User.CreatedAt)}",
            };
        }

        public async Task<IdentityResult> ChangePasswordAsync(string id, ChangePasswordSchema schema)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", id);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "Người dùng không tồn tại"
                });
            }

            return await _userManager.ChangePasswordAsync(user, schema.CurrentPassword, schema.NewPassword);
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", id);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "Người dùng không tồn tại"
                });
            }

            if (user.Email == IdentityDataSeeder.DefaultAdminUser.Email)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DeleteDefaultAdmin",
                    Description = "Không thể xoá tài khoản Admin mặc định"
                });
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<PaginationResponse<UserResponse>> GetAllAsync(int page, int size)
        {
            var users = await _userManager.Users.ToListAsync();

            return new PaginationResponse<UserResponse>(
                page, size,
                users.Count,
                users.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToUserResponse()).ToList());
        }

        public async Task<PaginationResponse<UserResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            List<string> search,
            List<string> order)
        {
            var query = _context.Users.AsQueryable();

            foreach (var item in search)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    query = query.Where(p =>
                        EF.Functions.Like(p.FullName, $"%{item}%") ||
                        EF.Functions.Like(p.PhoneNumber, $"%{item}%") ||
                        EF.Functions.Like(p.Email, $"%{item}%"));
                }
            }

            if (order.Any(s => !string.IsNullOrEmpty(s)))
                query = OrderHelper.OrderByMultiple<User>(query, order, _orderable);
            else
                query = query.OrderByDescending(u => u.CreatedAt);

            var users = await query.ToListAsync();

            return new PaginationResponse<UserResponse>(
                page, size,
                users.Count,
                users.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToUserResponse()).ToList());
        }

        public async Task<UserResponse?> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", id);
                return null;
            }
            return user.ToUserResponse();
        }

        public async Task<UserFullDetailResponse?> GetProfileByIdAsync(string id)
        {
            var user = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                _logger.LogError("User not found: {0}", id);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDetail = user.ToUserFullDetailResponse();
            userDetail.Roles = roles;

            return userDetail;
        }

        public async Task<IdentityResult> SetPasswordAsync(string currentUserId, string userToSetId, AdminSetPasswordSchema schema)
        {
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            if (currentUser == null)
            {
                _logger.LogError("Unauthorized: {0}", currentUserId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Unauthorized",
                    Description = "Unauthorized"
                });
            }

            var isAuthorized = await _userManager.CheckPasswordAsync(currentUser, schema.YourPassword);
            if (!isAuthorized)
            {
                _logger.LogError("Logged in user password not correct: {0}", currentUserId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "YourPasswordNotCorrect",
                    Description = "Mật khẩu của bạn không chính xác"
                });
            }

            var userToSet = await _userManager.FindByIdAsync(userToSetId);
            if (userToSet == null)
            {
                _logger.LogError("User not found: {0}", userToSetId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "Người dùng không tồn tại"
                });
            }

            if (userToSet.Email == IdentityDataSeeder.DefaultAdminUser.Email)
            {
                _logger.LogError("Set default admin password not allowed: {0}", userToSetId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "SetDefaultAdminPassword",
                    Description = "Không thể đặt mật khẩu cho tài khoản Admin mặc định"
                });
            }

            var removePasswordResult = await _userManager.RemovePasswordAsync(userToSet);
            if (!removePasswordResult.Succeeded)
            {
                _logger.LogError("Remove password failed: {0}", userToSetId);
                return removePasswordResult;
            }

            return await _userManager.AddPasswordAsync(userToSet, schema.NewPassword);
        }

        public async Task<UserFullDetailResponse?> UpdateProfileAsync(string id, UserUpdateSchema schema)
        {
            var user = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                _logger.LogError("User not found: {0}", id);
                throw new ArgumentNullException("Người dùng không tồn tại");
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
                }
            }

            user.FullName = schema.FullName;
            user.PhoneNumber = schema.PhoneNumber;

            if (user.UserProfile == null)
            {
                user.UserProfile = new UserProfile();
                user.UserProfile.UserId = id;
                _context.UserProfiles.Add(user.UserProfile);
                _context.SaveChanges();
            }

            user.UserProfile.Gender = schema.Gender;
            user.UserProfile.DateOfBirth = schema.DateOfBirth;
            user.UserProfile.CurrentRank = schema.CurrentRank;
            user.UserProfile.Address = schema.Address;
            user.UserProfile.JoinDate = schema.JoinDate;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDetail = user.ToUserFullDetailResponse();
                userDetail.Roles = roles;

                return userDetail;
            }

            return null;
        }

        public async Task<IdentityResult> UpdateRolesAsync(string targetTd, IEnumerable<string> newRoles)
        {
            var target = _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == targetTd);

            if (target != null)
            {
                if (target.Email == IdentityDataSeeder.DefaultAdminUser.Email)
                {
                    _logger.LogError("Update admin roles not allowed: {0}", targetTd);
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "UpdateDefaultAdminRoles",
                        Description = "Không thể cập nhật vai trò cho tài khoản Admin mặc định"
                    });
                }

                var roles = await _userManager.GetRolesAsync(target);
                var validRoles = Enum.GetValues(typeof(Role)).Cast<Role>();
                var invalidRoles = newRoles.Where(role => !validRoles.Any(validRole => validRole.ToString() == role)).ToList();

                if (invalidRoles.Any()) return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRoles",
                    Description = $"Vai trò không hợp lệ: '{string.Join("' | '", invalidRoles)}'"
                });

                var removeRolesResult = await _userManager.RemoveFromRolesAsync(target, roles);
                if (removeRolesResult.Succeeded) return await _userManager.AddToRolesAsync(target, newRoles);

                _logger.LogError("Remove roles failed: {0}", targetTd);
                return removeRolesResult;
            }

            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "Người dùng không tồn tại"
            });
        }
    }
}
