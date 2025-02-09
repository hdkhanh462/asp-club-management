using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;

namespace IctuTaekwondo.Shared.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<JwtResponse?> LoginAsync(LoginSchema schema)
        {
            var response = await _apiService.PostAsync<JwtResponse>("api/auth/login", schema.ToStringContent());
            return response.Data;
        }
    }
}
