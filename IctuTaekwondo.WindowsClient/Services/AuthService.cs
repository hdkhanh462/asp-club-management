using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;

namespace IctuTaekwondo.WindowsClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiService _apiService;

        public AuthService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<JwtResponse?> LoginAsync(LoginSchema schema)
        {
            var response = await _apiService.PostAsync<JwtResponse>("api/auth/login", schema.ToStringContent());
            return response.Data;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterSchema schema)
        {
            throw new NotImplementedException();
        }
    }
}
