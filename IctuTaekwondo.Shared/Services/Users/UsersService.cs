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
                var errors = new Dictionary<string, string[]>();

                if (!string.IsNullOrEmpty(response.Message) && response.Errors.Count == 0)
                    errors.Add(string.Empty, [response.Message]);

                if (response.Errors.Count > 0)
                {
                    foreach (var error in response.Errors)
                    {
                        errors.Add(error.Key, error.Value);
                    }
                }
                return errors;
            }

            return [];
        }

        public async Task<string?> DeleteAsync(string token, string id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/users/{id}");
            if (response.StatusCode != HttpStatusCode.OK) 
                return response.Message;
            return null;
        }

        public async Task<Paginator<UserResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null)
        {
            apiService.SetAuthorizationHeader(token);

            query = query ?? new QueryBuilder();
            query.Add("page", page.ToString());
            query.Add("size", size.ToString());

            var response = await apiService.GetAsync<Paginator<UserResponse>>($"api/users/filter{query.ToQueryString()}");
            return response.Data ?? Paginator<UserResponse>.GetDefaultInstance();
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
