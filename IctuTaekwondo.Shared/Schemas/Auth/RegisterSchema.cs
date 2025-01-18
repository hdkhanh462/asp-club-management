using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class RegisterSchema
    {
        [DataType(DataType.ImageUrl)]
        public string? AvatarUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password",ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class RegisterAdminSchema : RegisterSchema
    {
        [Required]
        public Role Role { get; set; }
    }
}
