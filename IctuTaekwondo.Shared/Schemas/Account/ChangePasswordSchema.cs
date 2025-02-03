using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class ChangePasswordSchema : SetPasswordSchema
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Mật khẩu cũ không được để trống")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
