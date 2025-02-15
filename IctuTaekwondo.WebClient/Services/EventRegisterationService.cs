using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.Shared;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IEventRegisterationService
    {
        public Task<bool> RegisterAsync(int eventId, HttpRequest request, ModelStateDictionary modelState);
        public Task<bool> UnregisterAsync(int eventId, HttpRequest request);
        public Task<bool> ManagerRegisterAsync(int eventId, string userId, HttpRequest request);
        public Task<bool> ManagerUnregisterAsync(int eventId, string userId, HttpRequest request);
        public Task<Paginator<ResgiteredUsersResponse>> GetAllAsync(int id, int page, int size, HttpRequest request);
    }

    public class EventRegisterationService : IEventRegisterationService
    {
        private readonly ILogger<EventService> _logger;

        private readonly ApiService _apiService;

        public EventRegisterationService(ILogger<EventService> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<Paginator<ResgiteredUsersResponse>> GetAllAsync(int id, int page, int size, HttpRequest request)
        {
            var builder = new QueryBuilder
            {
                { nameof(page), page.ToString() },
                { nameof(size), size.ToString() },
            };

            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var response = await _apiService.GetAsync<Paginator<ResgiteredUsersResponse>>($"api/events/{id}/registerations{builder.ToQueryString()}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    return response.Data;
            }
            return new Paginator<ResgiteredUsersResponse>(page, size, 0, []);
        }

        public async Task<bool> ManagerRegisterAsync(int eventId, string userId, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var apiResponse = await _apiService.PostAsync<object>($"api/events/{eventId}/registerations/{userId}", string.Empty.ToStringContent());
            return apiResponse.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> ManagerUnregisterAsync(int eventId, string userId, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var apiResponse = await _apiService.DeleteAsync<object>($"api/events/{eventId}/registerations/{userId}");
            return apiResponse.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> RegisterAsync(int eventId, HttpRequest request, ModelStateDictionary modelState)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var apiResponse = await _apiService.PostAsync<object>($"api/events/{eventId}/register", string.Empty.ToStringContent());
            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                if (apiResponse.Message != null && apiResponse.Errors == null)
                    modelState.AddModelError(string.Empty, apiResponse.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> UnregisterAsync(int eventId, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader(authToken ?? string.Empty);

            var apiResponse = await _apiService.DeleteAsync<object>($"api/events/{eventId}/unregister");
            return apiResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}
