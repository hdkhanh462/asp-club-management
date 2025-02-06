using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
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
        Task<UserResponse?> GetByIdAsync(string id);
        Task<UserFullDetailResponse?> GetProfileByIdAsync(string id);
        Task<PaginationResponse<UserResponse>> GetAllAsync(int page, int size);
        Task<IdentityResult> ChangePasswordAsync(string id, ChangePasswordSchema schema);
        Task<IdentityResult> SetPasswordAsync(string id, SetPasswordSchema schema);
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

        public async Task<UserResponse?> GetByIdAsync(string id)
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

        public async Task<IdentityResult> SetPasswordAsync(string id, SetPasswordSchema schema)
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

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                _logger.LogError("Remove password failed: {0}", id);
                return removePasswordResult;
            }

            return await _userManager.AddPasswordAsync(user, schema.NewPassword);
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
    }
}
