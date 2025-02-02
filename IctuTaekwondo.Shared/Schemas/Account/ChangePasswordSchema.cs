using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class ChangePasswordSchema : SetPasswordSchema
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
