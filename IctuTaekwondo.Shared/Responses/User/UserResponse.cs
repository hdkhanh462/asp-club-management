using System.ComponentModel;

namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserResponse
    {
        public string Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string? UserName { get; set; }
        
        [DisplayName("Địa chỉ email")]
        public string? Email { get; set; }
        
        [DisplayName("Só điện thoại")]
        public string? PhoneNumber { get; set; }
        
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
