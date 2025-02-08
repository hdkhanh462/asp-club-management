using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Utils;

namespace IctuTaekwondo.Shared.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IApiService apiService;

        public AccountService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<bool> ChangePasswordAsync(string token, ChangePasswordSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<object>("api/account/change-password", schema.ToStringContent());
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(string token)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<UserFullDetailResponse>("api/account/profile");
            return response.Data;
        }

        public async Task<bool> UpdateProfileAsync(string token, UserUpdateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<object>("api/account/profile", schema.ToStringContent());
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
