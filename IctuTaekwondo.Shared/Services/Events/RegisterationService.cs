using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Events
{
    public class RegisterationService : IRegisterationService
    {
        private readonly IApiService apiService;

        public RegisterationService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<Paginator<ResgiteredUsersResponse>> GetAllAsync(string token, int id, int page, int size)
        {
            var query = new QueryBuilder
            {
                { "page", page.ToString() },
                { "size", size.ToString() },
            };

            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<Paginator<ResgiteredUsersResponse>>($"api/events/{id}/registerations{query.ToQueryString()}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    return response.Data;
            }
            return Paginator<ResgiteredUsersResponse>.GetDefaultInstance();
        }

        public Task<bool> ManagerRegisterAsync(string token, int eventId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ManagerUnregisterAsync(string token, int eventId, string userId)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/events/{eventId}/registerations/{userId}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<string?> RegisterAsync(string token, int eventId)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PostAsync<object>($"api/events/{eventId}/register", string.Empty.ToStringContent());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return response.Message;
            }
            return null;
        }

        public async Task<bool> UnregisterAsync(string token, int eventId)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/events/{eventId}/unregister");
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
