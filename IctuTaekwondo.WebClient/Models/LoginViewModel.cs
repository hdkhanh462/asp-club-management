using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Schemas.Auth;

namespace IctuTaekwondo.WebClient.Models
{
    public class LoginViewModel : LoginSchema
    {
        [Display(Name = "Lưu phiên đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
