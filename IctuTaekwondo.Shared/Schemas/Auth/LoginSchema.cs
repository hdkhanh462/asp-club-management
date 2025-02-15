using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class LoginSchema
    {
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Định dạng email không chính xác")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lưu phiên đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
