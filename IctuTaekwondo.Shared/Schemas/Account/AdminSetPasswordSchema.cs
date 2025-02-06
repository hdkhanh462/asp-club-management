using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class AdminSetPasswordSchema : SetPasswordSchema
    {
        [Display(Name = "Mật khẩu của bạn")]
        [Required(ErrorMessage = "Mật khẩu của bạn không được để trống")]
        [DataType(DataType.Password)]
        public string YourPassword { get; set; }
    }
}
