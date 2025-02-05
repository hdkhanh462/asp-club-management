using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class UserUpdateSchema : UserProfileSchema
    {
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải có độ dài từ 6 đến 50 ký tự")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [FileUpload(MaxFileSizeMb = 2)]
        public IFormFile? Avatar { get; set; }
        public string? AvatarUrl { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>
            {
                { "FullName", FullName },
            };

            if (PhoneNumber != null) dict["PhoneNumber"] = PhoneNumber;
            if (Avatar != null) dict["Avatar"] = Avatar;
            if (Gender != null) dict["Gender"] = Gender;
            if (DateOfBirth != null) dict["DateOfBirth"] = DateOfBirth;
            if (Address != null) dict["Address"] = Address;
            if (CurrentRank != null) dict["CurrentRank"] = CurrentRank;
            if (JoinDate != null) dict["JoinDate"] = JoinDate;

            return dict;
        }
    }

    public class AdminUserUpdateSchema : UserUpdateSchema
    {
        public string Id { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string? Email { get; set; }
    }
}
