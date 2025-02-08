using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;

namespace IctuTaekwondo.WindowsClient.Services
{
    public interface IAuthService
    {
        Task<JwtResponse?> LoginAsync(LoginSchema schema);
        Task<bool> RegisterAsync(RegisterSchema schema);
        Task LogoutAsync();
    }
}
