using System.Drawing;
using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IUserService : ICallApiService
    {
        public Task<Paginator<UserResponse>?> GetAllAsync(
            int page,
            int size,
            List<string> search,
            List<string> order,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
        public Task<UserFullDetailResponse?> GetProfileByIdAsync(string id, HttpRequest request);
        public Task<UserFullDetailResponse?> UpdateAsync(
            string id,
            UserUpdateSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
        public Task<string?> DeleteAsnyc(
            string id,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
        public Task<bool> SetPasswordAsync(
            string currentUserId,
            string userToSetId,
            AdminSetPasswordSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
        public Task<bool> UpdateRolesAsync(
            string currentId,
            string targetId,
            UpdateRolesSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly ApiHelper _apiHelper;
        private readonly ApiService _apiService;

        public UserService(ILogger<UserService> logger, ApiHelper apiHelper, ApiService apiService)
        {
            _logger = logger;
            _apiHelper = apiHelper;
            _apiService = apiService;
        }

        public async Task<string?> DeleteAsnyc(
            string id,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}"
                }
            });

            var response = await _apiHelper.DeleteAsync<string>($"api/users/{id}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<string>(response, modelState);
            }
            return response.Data;
        }

        public async Task<Paginator<UserResponse>?> GetAllAsync(
            int page,
            int size,
            List<string> search,
            List<string> order,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}"
                }
            });

            var response = await _apiHelper
                .GetAsync<Paginator<UserResponse>>($"api/users/filter?" +
                $"page={page}&" +
                $"size={size}&" +
                $"search={string.Join("&search=", search)}&" +
                $"order={string.Join("&order=", order)}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<Paginator<UserResponse>>(response, modelState);
                return null;
            }
            return response.Data;
        }

        public async Task<UserFullDetailResponse?> GetProfileByIdAsync(string id, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var response = await _apiService.GetAsync<UserFullDetailResponse>($"api/users/{id}");
            return response.Data;
        }

        public void HandleErrors<T>(ApiResponse<T> response, ModelStateDictionary modelState)
        {
            if (response.Message != null && response.Errors == null) modelState.AddModelError(string.Empty, response.Message);
            if (response.Errors != null)
            {
                foreach (var (key, value) in response.Errors)
                {
                    var keyName = key;
                    if (key.Contains("Password")) keyName = "ConfirmNewPassword";
                    if (key.Contains("YourPassword")) keyName = "YourPassword";

                    foreach (var error in value)
                    {
                        modelState.AddModelError(keyName, error);
                    }
                }
            }
            _logger.LogError("Status code: {StatusCode}, Message: {Message}", response.StatusCode, response.Message);
        }

        public async Task<UserFullDetailResponse?> UpdateAsync(
            string id,
            UserUpdateSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}"
                }
            });

            var response = await _apiHelper.PutAsync<UserFullDetailResponse>(
                $"api/users/{id}/profile",
                schema.ToDictionary(),
                "multipart/form-data");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<UserFullDetailResponse>(response, modelState);
                return null;
            }
            return response.Data;
        }

        public async Task<bool> SetPasswordAsync(
            string currentUserId,
            string userToSetId,
            AdminSetPasswordSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}"
                }
            });
            var response = await _apiHelper.PutAsync<object>(
                $"api/users/{userToSetId}/set-password",
                schema);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<object>(response, modelState);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateRolesAsync(
            string currentId,
            string targetId,
            UpdateRolesSchema schema,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies)
        {
            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}"
                }
            });
            var response = await _apiHelper.PutAsync<object>(
                $"api/users/{targetId}/roles",
                schema);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<object>(response, modelState);
                return false;
            }
            return true;
        }
    }
}
