using System.Drawing;
using System.Net;
using System.Xml.Linq;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IEventService
    {
        public Task<EventResponse?> CreateAsync(EventCreateSchema schema, ModelStateDictionary modelState, HttpRequest request);
        public Task<EventResponse?> UpdateAsync(int id, EventUpdateSchema schema, ModelStateDictionary modelState, HttpRequest request);
        public Task<bool> DeleteAsync(int id, HttpRequest request);
        public Task<EventResponse?> FindByIdAsync(int id, HttpRequest request);
        public Task<PaginationResponse<EventResponse>> GetAllAsync(int page, int size, ModelStateDictionary modelState, HttpRequest request);
        public Task<PaginationResponse<EventResponse>> FilterAsync(int page, int size, string? name = null, EventStatus? status = null);
    }

    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;

        private readonly ApiService _apiService;

        public EventService(ILogger<EventService> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<EventResponse?> CreateAsync(EventCreateSchema schema, ModelStateDictionary modelState, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader("Bearer", authToken ?? string.Empty);

            var apiResponse = await _apiService.PostAsync<EventResponse>("api/events", schema.ToStringContent());
            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                if (apiResponse.Message != null && apiResponse.Errors == null)
                    modelState.AddModelError(string.Empty, apiResponse.Message);
                if (apiResponse.Errors != null)
                    modelState.AddError(apiResponse.Errors);
                return null;
            }
            return apiResponse.Data;
        }

        public async Task<bool> DeleteAsync(int id, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader("Bearer", authToken ?? string.Empty);

            var response = await _apiService.DeleteAsync<EventResponse>($"api/events/{id}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<PaginationResponse<EventResponse>> FilterAsync(int page, int size, string? name = null, EventStatus? status = null)
        {
            var builder = new QueryBuilder
            {
                { nameof(page), page.ToString() },
                { nameof(size), size.ToString() },
                { nameof(name), name ?? string.Empty },
                { nameof(status), status.ToString() ?? string.Empty }
            };

            var response = await _apiService.GetAsync<PaginationResponse<EventResponse>>($"api/events/filter?{builder.ToQueryString()}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    return response.Data;
            }
            return new PaginationResponse<EventResponse>(page, size, 0, []);
        }

        public async Task<EventResponse?> FindByIdAsync(int id, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader("Bearer", authToken ?? string.Empty);

            var response = await _apiService.GetAsync<EventResponse>($"api/events/{id}");
            return response.Data;
        }

        public async Task<PaginationResponse<EventResponse>> GetAllAsync(int page, int size, ModelStateDictionary modelState, HttpRequest request)
        {
            var builder = new QueryBuilder
            {
                { nameof(page), page.ToString() },
                { nameof(size), size.ToString() },
            };

            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader("Bearer", authToken ?? string.Empty);

            var response = await _apiService.GetAsync<PaginationResponse<EventResponse>>($"api/events{builder.ToQueryString()}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    return response.Data;
            }
            return new PaginationResponse<EventResponse>(page, size, 0, []);
        }

        public async Task<EventResponse?> UpdateAsync(int id, EventUpdateSchema schema, ModelStateDictionary modelState, HttpRequest request)
        {
            var authToken = request.Cookies[GlobalConst.CookieAuthTokenKey];
            _apiService.SetAuthorizationHeader("Bearer", authToken ?? string.Empty);

            var response = await _apiService.PutAsync<EventResponse>($"api/events/{id}", schema.ToStringContent());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.Message != null && response.Errors == null)
                    modelState.AddModelError(string.Empty, response.Message);
                if (response.Errors != null)
                    modelState.AddError(response.Errors);
                return null;
            }

            return response.Data;
        }
    }
}
