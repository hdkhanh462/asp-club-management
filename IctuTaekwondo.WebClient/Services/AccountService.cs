using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.Shared.Schemas.Account;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IctuTaekwondo.Shared.Responses;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IAccountService
    {
        Task<UserFullDetailResponse?> GetProfileAsync(string token);
        Task<UserResponse?> GetUser(string token);
        Task<UserFullDetailResponse?> UpdateProfileAsync(string token, UserUpdateSchema schema);
        Task<bool> ChangePasswordAsync(string token, ChangePasswordSchema schema);
    }

    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IApiService _apiService;

        public AccountService(ILogger<AccountService> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.GetAsync<UserFullDetailResponse>($"api/account/profile");
            return response.Data;
        }

        public async Task<UserResponse?> GetUser(string token)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.GetAsync<UserResponse>("api/account/me");
            return response.Data;
        }

        public async Task<UserFullDetailResponse?> UpdateProfileAsync(string token, UserUpdateSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.PutAsync<UserFullDetailResponse>("api/account/profile", schema.ToMultipartFormDataContent());

            return response.Data;
        }

        public async Task<bool> ChangePasswordAsync(string token, ChangePasswordSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);

            var response = await _apiService.PutAsync<object>("api/account/change-password", schema.ToStringContent());
            return response.StatusCode == HttpStatusCode.OK;
        }

        public void HandleErrors<T>(ApiResponse<T> response, ModelStateDictionary modelState)
        {
            if (response.Message != null && response.Errors == null) modelState.AddModelError(string.Empty, response.Message);
            if (response.Errors != null)
            {
                foreach (var (key, value) in response.Errors)
                {
                    var keyName = string.Empty;
                    if (key.Contains("Password")) keyName = "ConfirmNewPassword";
                    if (key.Contains("PasswordMismatch")) keyName = "CurrentPassword";

                    foreach (var error in value)
                    {
                        modelState.AddModelError(keyName, error);
                    }
                }
            }
            _logger.LogError("Register failed with status code: {StatusCode}, Message: {Message}", response.StatusCode, response.Message);
        }
    }
}
