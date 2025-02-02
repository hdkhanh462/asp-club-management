using IctuTaekwondo.Shared.Schemas.Auth;

namespace IctuTaekwondo.WebClient.Models
{
    public class RegisterViewModel : RegisterAdminSchema
    {
        public Dictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>
            {
                { "FullName", FullName },
                { "Email", Email },
                { "Password", Password },
                { "ConfirmPassword", ConfirmPassword },
            };

            if (PhoneNumber != null) dict["PhoneNumber"] = PhoneNumber;
            if (Avatar != null) dict["Avatar"] = Avatar;
            if (Roles != null) dict["Roles"] = Roles.Select(x => (int)x).ToList();

            return dict;
        }
    }
}
