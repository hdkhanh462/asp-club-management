using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class RegisterSchema
    {
        [Display(Name = "Ảnh đại diện")]
        [FileUpload(MaxFileSizeMb = 2)]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải có độ dài từ 6 đến 50 ký tự")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 100 ký tự")]
        public string Password { get; set; } = null!;

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu của bạn")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không trùng khớp")]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class AdminRegisterSchema : RegisterSchema
    {
        [Display(Name = "Vai trò")]
        [EnumList(typeof(Role), ErrorMessage = "Vai trò không hợp lệ")]
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
