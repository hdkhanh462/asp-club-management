using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.Shared.Schemas.Account;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IctuTaekwondo.Shared.Responses;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IAccountService : ICallApiService
    {
        Task<UserFullDetailResponse?> GetProfileAsync(HttpRequest request);
        UserResponse? GetUser(IRequestCookieCollection requestCookies);
        Task<UserFullDetailResponse?> UpdateProfileAsync(UserUpdateSchema schema,
            ModelStateDictionary modelState, 
            IRequestCookieCollection requestCookies);
        Task<bool> ChangePasswordAsync(ChangePasswordSchema schema,
            ModelStateDictionary modelState, 
            IRequestCookieCollection requestCookies);
    }

    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly ApiHelper _apiHelper;
        private readonly ApiService _apiService;

        public AccountService(ILogger<AccountService> logger, ApiHelper apiHelper, ApiService apiService)
        {
            _logger = logger;
            _apiHelper = apiHelper;
            _apiService = apiService;
        }

        public async Task<UserFullDetailResponse?> GetProfileAsync(HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var response = await _apiService.GetAsync<UserFullDetailResponse>($"api/account/profile");
            return response.Data;
        }

        public UserResponse? GetUser(IRequestCookieCollection requestCookies)
        {
            if (!requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey)) return null;

            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]}"
            });

            return _apiHelper.GetAsync<UserResponse>("api/account/me").Result.Data;
        }

        public async Task<UserFullDetailResponse?> UpdateProfileAsync(UserUpdateSchema schema, 
            ModelStateDictionary modelState, 
            IRequestCookieCollection requestCookies)
        {
            if (!requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                modelState.AddModelError(string.Empty, "Không tìm thấy thông tin người dùng");
                return null;
            };

            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]}"
            });

            var response = await _apiHelper.PutAsync<UserFullDetailResponse>("api/account/profile", schema.ToDictionary(), "multipart/form-data");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<UserFullDetailResponse>(response, modelState);
                return null;
            };

            return response.Data;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            if (!requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                modelState.AddModelError(string.Empty, "Không tìm thấy thông tin người dùng");
                return false;
            };

            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]}"
            });

            var response = await _apiHelper.PutAsync<object>("api/account/change-password", schema);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<object>(response, modelState);
                return false;
            };

            return true;
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
