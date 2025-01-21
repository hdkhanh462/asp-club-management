using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class RegisterSchema
    {
        [DataType(DataType.ImageUrl)]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải có độ dài từ 6 đến 50 ký tự.")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 50 ký tự.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class RegisterAdminSchema : RegisterSchema
    {
        [Display(Name = "Vai trò")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        public Role Role { get; set; }
    }
}
