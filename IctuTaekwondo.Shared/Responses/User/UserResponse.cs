using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserResponse
    {
        public string Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string? UserName { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? AvatarUrl { get; set; }
    }
}
