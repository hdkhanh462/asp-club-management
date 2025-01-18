using Microsoft.AspNetCore.Identity;
using IctuTaekwondo.Api.Models;

namespace IctuTaekwondo.Api.Utils
{
    public class IdentityDataSeeder
    {
        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            // Lấy các dịch vụ Identity
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

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
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123"; // Đảm bảo tuân thủ các yêu cầu mật khẩu

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new User
                {
                    FullName = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Gán role Admin cho tài khoản
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
