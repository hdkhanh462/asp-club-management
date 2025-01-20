using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Auth
{
    public class LoginSchema
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
