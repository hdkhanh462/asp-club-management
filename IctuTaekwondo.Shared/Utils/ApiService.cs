using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses;

namespace IctuTaekwondo.Shared.Utils
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions SerializerOptions = new()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };

        public const string DefaultAuthorizationScheme = "Bearer";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            return await HandleApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string requestUri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(requestUri, content);
            return await HandleApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string requestUri, HttpContent content)
        {
            var response = await _httpClient.PutAsync(requestUri, content);
            return await HandleApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            return await HandleApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PostMultipartAsync<T>(string requestUri, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(requestUri, content);
            return await HandleApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PutMultipartAsync<T>(string requestUri, MultipartFormDataContent content)
        {
            var response = await _httpClient.PutAsync(requestUri, content);
            return await HandleApiResponse<T>(response);
        }

        public void SetAuthorizationHeader(string parameter, string scheme = DefaultAuthorizationScheme)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        }

        public void SetDefaultRequestHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var (key, value) in headers)
            {
                if (!_httpClient.DefaultRequestHeaders.Contains(key))
                {
                    _httpClient.DefaultRequestHeaders.Add(key, value);
                }
            }
        }

        private async Task<ApiResponse<T>> HandleApiResponse<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseContent))
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, SerializerOptions);

                if (apiResponse != null)
                {
                    apiResponse.StatusCode = response.StatusCode;
                }
                return apiResponse ?? new ApiResponse<T> { StatusCode = response.StatusCode };
            }
            return new ApiResponse<T> { StatusCode = response.StatusCode };
        }
    }
}
