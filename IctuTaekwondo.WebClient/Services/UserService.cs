using System.Drawing;
using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IUserService : ICallApiService
    {
        public Task<PaginationResponse<UserResponse>?> GetAllAsync(
            int page,
            int size,
            List<string> search,
            List<string> order,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
        public Task<UserFullDetailResponse?> GetFullDetailAsync(
            string id,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies);
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
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly ApiHelper _apiHelper;

        public UserService(ILogger<UserService> logger, ApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
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

        public async Task<PaginationResponse<UserResponse>?> GetAllAsync(
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
                .GetAsync<PaginationResponse<UserResponse>>($"api/users/filter?" +
                $"page={page}&" +
                $"size={size}&" +
                $"search={string.Join("&search=", search)}&" +
                $"order={string.Join("&order=", order)}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<PaginationResponse<UserResponse>>(response, modelState);
                return null;
            }
            return response.Data;
        }

        public async Task<UserFullDetailResponse?> GetFullDetailAsync(
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

            var response = await _apiHelper.GetAsync<UserFullDetailResponse>($"api/users/{id}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<UserFullDetailResponse>(response, modelState);
            }
            return response.Data;
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

            var response = await _apiHelper.PutAsync<UserFullDetailResponse>($"api/users/{id}/profile",
                schema.ToDictionary(), "multipart/form-data");
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
            var response = await _apiHelper.PutAsync<object>($"api/users/{userToSetId}/set-password",
                schema.ToDictionary());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<object>(response, modelState);
                return false;
            }
            return true;
        }
    }
}
