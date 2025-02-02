using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class SetPasswordSchema
    {
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu mới")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmNewPassword { get; set; }
    }
}
