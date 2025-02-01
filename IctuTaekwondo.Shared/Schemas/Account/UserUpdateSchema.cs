using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class UserUpdateSchema : UserProfileSchema
    {
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải có độ dài từ 6 đến 50 ký tự.")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Định dạng địa chỉ avatar không chính xác")]
        public string? AvatarUrl { get; set; }
    }

    public class AdminUserUpdateSchema : UserUpdateSchema
    {
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
    }
}
