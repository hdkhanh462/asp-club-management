using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Winforms.Models.Users
{
    public class UserResponse
    {
        public string Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        
        [DisplayName("Địa chỉ email")]
        public string Email { get; set; }
        
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
