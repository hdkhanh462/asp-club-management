using System.Net;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IApiService apiService;

        public UsersService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<Dictionary<string, string[]>> RegisterAsync(string token,AdminRegisterSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PostAsync<object>("api/auth/register", schema.ToMultipartFormDataContent());
            
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return response.Errors;
            }

            return [];
        }

        public async Task<string?> DeleteAsync(string token, string id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/users/{id}");
            return response.Message;
        }

        public async Task<PaginationResponse<UserResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null)
        {
            apiService.SetAuthorizationHeader(token);

            query = query ?? new QueryBuilder();
            query.Add("page", page.ToString());
            query.Add("size", size.ToString());

            var response = await apiService.GetAsync<PaginationResponse<UserResponse>>($"api/users{query.ToQueryString()}");
            return response.Data ?? PaginationResponse<UserResponse>.GetDefaultInstance();
        }

        public async Task<UserFullDetailResponse?> GetProfileByIdAsync(string token, string id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<UserFullDetailResponse>($"api/users/{id}");
            return response.Data;
        }

        public async Task<bool> SetPasswordAsync(string token, string id, AdminSetPasswordSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<object>($"api/users/{id}/set-password", schema.ToStringContent());
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<UserFullDetailResponse?> UpdateAsync(string token, string id, UserUpdateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutMultipartAsync<UserFullDetailResponse>($"api/users/{id}/profile", schema.ToMultipartFormDataContent());
            return response.Data;
        }

        public async Task<bool> UpdateRolesAsync(string token, string id, UpdateRolesSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<object>($"api/users/{id}/roles", schema.ToStringContent());
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
