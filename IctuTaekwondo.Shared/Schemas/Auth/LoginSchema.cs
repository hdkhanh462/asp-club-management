using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class LoginSchema
    {
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
