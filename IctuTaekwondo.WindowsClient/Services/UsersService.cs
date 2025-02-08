using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApiService _apiService;

        public UsersService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> DeleteAsync(string token, string id)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.DeleteAsync<ApiResponse<object>>($"api/users/{id}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<UserFullDetailResponse?> FindByIdAsync(string token, string id)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.GetAsync<UserFullDetailResponse>($"api/users/{id}");
            return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
        }

        public async Task<PaginationResponse<UserResponse>> GetAllSync(string token, QueryBuilder? queryBuilder = null)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.GetAsync<PaginationResponse<UserResponse>>($"api/users/filter{queryBuilder?.ToQueryString()}");
            return response.StatusCode == HttpStatusCode.OK && response.Data != null ? response.Data : PaginationResponse<UserResponse>.GetDefaultInstance();
        }

        public async Task<UserFullDetailResponse?> GetProfileByIdAsync(string token, string id)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.GetAsync<UserFullDetailResponse>($"api/users/profile/{id}");
            return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
        }

        public async Task<bool> RegisterAsync(string token, AdminRegisterSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.PostAsync<ApiResponse<object>>("api/users/register", new StringContent(JsonSerializer.Serialize(schema), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> SetPasswordAsync(string token, string id, AdminSetPasswordSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.PutAsync<ApiResponse<object>>($"api/users/{id}/set-password", new StringContent(JsonSerializer.Serialize(schema), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<UserFullDetailResponse?> UpdateAsync(string token, AdminUserUpdateSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.PutAsync<UserFullDetailResponse>($"api/users/{schema.Id}", new StringContent(JsonSerializer.Serialize(schema), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
        }

        public async Task<bool> UpdateRolesAsync(string token, string id, UpdateRolesSchema schema)
        {
            _apiService.SetAuthorizationHeader(token);
            var response = await _apiService.PutAsync<ApiResponse<object>>($"api/users/{id}/roles", new StringContent(JsonSerializer.Serialize(schema), Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
