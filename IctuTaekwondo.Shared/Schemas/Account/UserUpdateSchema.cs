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
        public string? PhoneNumber { get; set; }

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? AvatarUrl { get; set; }
    }

    public class AdminUserUpdateSchema : UserUpdateSchema
    {
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống.")]
        public string Email { get; set; }
    }
}
