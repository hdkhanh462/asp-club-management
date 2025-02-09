using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;

namespace IctuTaekwondo.Shared.Services.Auth
{
    public interface IAuthService
    {
        Task<JwtResponse?> LoginAsync(LoginSchema schema);
        //Task<bool> LogoutAsync();
    }
}
