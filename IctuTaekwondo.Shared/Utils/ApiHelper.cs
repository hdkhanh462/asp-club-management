using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IctuTaekwondo.Shared.Responses;

namespace IctuTaekwondo.Shared.Utils;

public class ApiHelper
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ApiHelper(HttpClient httpClient, JsonSerializerOptions? jsonSerializerOptions =null)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public Task<ApiResponse<T>> GetAsync<T>(string endpoint) =>
        ExecuteApiRequest<T>(() => _httpClient.GetAsync(endpoint));

    public Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data) =>
        ExecuteApiRequest<T>(() =>
        {
            var content = CreateJsonContent(data);
            return _httpClient.PostAsync(endpoint, content);
        });

    public Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data) =>
        ExecuteApiRequest<T>(() =>
        {
            var content = CreateJsonContent(data);
            return _httpClient.PutAsync(endpoint, content);
        });

    public Task<ApiResponse<T>> DeleteAsync<T>(string endpoint) =>
        ExecuteApiRequest<T>(() => _httpClient.DeleteAsync(endpoint));

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

    private static StringContent CreateJsonContent(object data) =>
        new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

    private async Task<ApiResponse<T>> ExecuteApiRequest<T>(Func<Task<HttpResponseMessage>> request)
    {
        try
        {
            using var response = await request();
            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        try
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, _jsonSerializerOptions);
            return apiResponse ?? CreateDefaultResponse<T>(response);
        }
        catch (JsonException)
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Invalid JSON: {responseContent}"
            };
        }
    }

    private static ApiResponse<T> CreateDefaultResponse<T>(HttpResponseMessage response) =>
        new ApiResponse<T>
        {
            StatusCode = response.StatusCode,
            Message = response.ReasonPhrase
        };
}
