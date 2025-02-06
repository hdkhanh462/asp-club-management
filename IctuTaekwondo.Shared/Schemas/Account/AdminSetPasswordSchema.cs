using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class AdminSetPasswordSchema : SetPasswordSchema
    {
        public string Id { get; set; }

        [Display(Name = "Mật khẩu của bạn")]
        [Required(ErrorMessage = "Mật khẩu của bạn không được để trống")]
        [DataType(DataType.Password)]
        public string YourPassword { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", Id },
                { "YourPassword", YourPassword },
                { "NewPassword", NewPassword },
                { "ConfirmNewPassword", ConfirmNewPassword },
            };

            return dict;
        }
    }
}
