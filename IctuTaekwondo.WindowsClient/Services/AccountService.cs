using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Utils;

namespace IctuTaekwondo.WindowsClient.Services
{

    public class AccountService : IAccountService
    {
        private readonly ApiService _apiService;

        public AccountService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.GetAsync<UserFullDetailResponse>("api/account/profile");
            return response.Data;
        }

        public async Task<bool> UpdateProfileAsync(UserFullDetailResponse userProfile, string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.PutAsync<UserFullDetailResponse>("api/account/profile", new StringContent(JsonSerializer.Serialize(userProfile), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword, string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var changePasswordSchema = new ChangePasswordSchema
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };

            var response = await _apiService.PostAsync<ChangePasswordSchema>("api/account/change-password", new StringContent(JsonSerializer.Serialize(changePasswordSchema), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAccountAsync(string userId, string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.DeleteAsync<UserResponse>($"api/account/{userId}");
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
