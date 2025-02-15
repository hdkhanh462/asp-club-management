using System.Net;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Achievements
{
    public class AchievementsService : IAchievementsService
    {
        private readonly IApiService apiService;

        public AchievementsService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<Dictionary<string, string[]>> CreateAsync(string token, AchievementCreateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PostAsync<AchievementResponse>($"api/achievements", schema.ToStringContent());
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return response.Errors;
            }

            return [];
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/achievements/{id}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<AchievementResponse?> FindByIdAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<AchievementResponse>($"api/achievements/{id}");
            return response.Data;
        }

        public async Task<Paginator<AchievementResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null)
        {
            apiService.SetAuthorizationHeader(token);

            query = query ?? new QueryBuilder();
            query.Add("page", page.ToString());
            query.Add("size", size.ToString());

            var response = await apiService.GetAsync<Paginator<AchievementResponse>>($"api/achievements{query.ToQueryString()}");
            return response.Data ?? Paginator<AchievementResponse>.GetDefaultInstance();
        }

        public async Task<AchievementResponse?> UpdateAsync(string token, int id, AchievementUpdateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<AchievementResponse>($"api/achievements/{id}", schema.ToStringContent());
            return response.Data;
        }
    }
}
