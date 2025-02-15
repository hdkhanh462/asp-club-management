using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Events
{
    public class EventsService : IEventsService
    {
        private readonly IApiService apiService;

        public EventsService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<Dictionary<string, string[]>> CreateAsync(string token, EventCreateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PostAsync<EventResponse>($"api/events",schema.ToStringContent());
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return response.Errors;
            }

            return [];
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/events/{id}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<EventResponse?> FindByIdAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<EventResponse>($"api/events/{id}");
            return response.Data;
        }

        public async Task<Paginator<EventResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null)
        {
            apiService.SetAuthorizationHeader(token);

            query = query ?? new QueryBuilder();
            query.Add("page", page.ToString());
            query.Add("size", size.ToString());

            var response = await apiService.GetAsync<Paginator<EventResponse>>($"api/events/filter{query.ToQueryString()}");
            return response.Data ?? Paginator<EventResponse>.GetDefaultInstance();
        }

        public async Task<EventResponse?> UpdateAsync(string token, int id, EventUpdateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<EventResponse>($"api/events/{id}", schema.ToStringContent());
            return response.Data;
        }
    }
}
