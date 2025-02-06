using Microsoft.AspNetCore.Identity;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Data
{
    public class IdentityDataSeeder
    {
        public static readonly User DefaultAdminUser = new User
        {
            FullName = "Admin",
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            // Lấy các dịch vụ Identity
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<ApiDbContext>();

            // Danh sách các roles cần seed
            var roles = new[] { "Admin", "Manager", "Member" };

            // Seed roles nếu chưa tồn tại
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed tài khoản Admin mặc định
            var adminPassword = "Admin@123"; // Đảm bảo tuân thủ các yêu cầu mật khẩu

            if (await userManager.FindByEmailAsync(DefaultAdminUser.Email!) == null)
            {
                DefaultAdminUser.CreatedAt = DateTime.Now;

                var result = await userManager.CreateAsync(DefaultAdminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Gán role Admin cho tài khoản
                    await userManager.AddToRoleAsync(DefaultAdminUser, "Admin");

                    // Tạo UserProfile cho tài khoản Admin
                    var userProfile = new UserProfile
                    {
                        User = DefaultAdminUser,
                        Gender = Gender.Male,
                        CurrentRank = RankName.BlackBelt,
                        JoinDate = new DateOnly(2020, 2, 2),
                    };

                    context.UserProfiles.Add(userProfile);
                    await context.SaveChangesAsync();

                    // Tạo một số dữ liệu mẫu khác ở đây
                }
            }
        }
    }
}
