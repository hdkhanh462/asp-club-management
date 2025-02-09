using System.Collections.Generic;

namespace IctuTaekwondo.Winforms.Models.Users
{
    public class UserWithProfileResponse : UserResponse
    {
        public virtual ICollection<string> Roles { get; set; } = new List<string>();

        public ProfileResponse Profile { get; set; }
    }
}
