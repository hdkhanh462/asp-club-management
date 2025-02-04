using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public Task<bool> DeleteAsnyc(
            int  id, 
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

        public async Task<bool> DeleteAsnyc(
            int id, 
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

            var response = await _apiHelper.DeleteAsync<bool>($"api/users/delete/{id}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<bool>(response, modelState);
                return false;
            }
            return true;
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

        public void HandleErrors<T>(ApiResponse<T> response, ModelStateDictionary modelState)
        {
            if (response.Message != null && response.Errors == null) modelState.AddModelError(string.Empty, response.Message);
            if (response.Errors != null)
            {
                foreach (var (key, value) in response.Errors)
                {
                    var keyName = string.Empty;

                    foreach (var error in value)
                    {
                        modelState.AddModelError(keyName, error);
                    }
                }
            }
            _logger.LogError("Status code: {StatusCode}, Message: {Message}", response.StatusCode, response.Message);
        }
    }
}
