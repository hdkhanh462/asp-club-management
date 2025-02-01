using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Schemas.Event;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class FileUpload : ValidationAttribute
    {
        public int MaxFileSizeMb { get; set; } = 1;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userObj = (UserUpdateSchema)validationContext.ObjectInstance;

            var maxFileSizeByte = MaxFileSizeMb * 1024 * 1024;
            var fileSize = userObj.Avatar?.Length / 1024 / 1024;

            if (fileSize != null && userObj.Avatar?.Length > maxFileSizeByte)
            {
                return new ValidationResult($"Kích thước tệp không được vượt quá {MaxFileSizeMb}Mb. " +
                    $"Kích thước hiện tại: {Math.Round((decimal)fileSize, 2)}Mb");
            }

            return ValidationResult.Success!;
        }
    }

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
        [FileUpload(MaxFileSizeMb = 2)]
        public IFormFile? Avatar { get; set; }
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
