using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Schemas.Account;

namespace IctuTaekwondo.WebClient.Models
{
    public class UpdateUserViewModel : UserUpdateSchema
    {
        public string Id { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
