using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class RegisterSchema : UserProfileSchema
    {
        [Display(Name = "Ảnh đại diện")]
        [FileUpload(MaxFileSizeMb = 2)]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải có độ dài từ 6 đến 50 ký tự.")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

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
        [ValidEnumList(EnumType = typeof(Role), ErrorMessage = "Vai trò không hợp lệ.")]
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
