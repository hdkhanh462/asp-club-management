using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Schemas.Auth;

namespace IctuTaekwondo.WebClient.Models
{
    public class LoginViewModel:LoginSchema
    {
        [Required]
        public bool RememberMe { get; set; }
    }
}
