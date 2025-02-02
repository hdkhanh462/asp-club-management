using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IctuTaekwondo.Shared.Utils;

public class ApiHelper
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ApiHelper(HttpClient httpClient, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public Task<ApiResponse<T>> GetAsync<T>(string endpoint) =>
        ExecuteApiRequest<T>(() => _httpClient.GetAsync(endpoint));

    public Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data, string mediaType = "application/json") =>
        ExecuteApiRequest<T>(() =>
        {
            var content = CreateContent(data, mediaType);
            return _httpClient.PostAsync(endpoint, content);
        });

    public Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data, string mediaType = "application/json") =>
        ExecuteApiRequest<T>(() =>
        {
            var content = CreateContent(data, mediaType);
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

    private static HttpContent CreateContent(object data, string mediaType)
    {
        if (mediaType == "application/json")
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, mediaType);
        }
        else if (mediaType == "application/x-www-form-urlencoded")
        {
            var formData = data as Dictionary<string, string> ?? throw new ArgumentException("Data must be a Dictionary<string, string> for form-urlencoded media type.");
            return new FormUrlEncodedContent(formData);
        }
        else if (mediaType.StartsWith("text/"))
        {
            return new StringContent(data.ToString() ?? string.Empty, Encoding.UTF8, mediaType);
        }
        else if (mediaType == "multipart/form-data")
        {
            var multipartContent = new MultipartFormDataContent();
            if (data is Dictionary<string, object> formData)
            {
                foreach (var (key, value) in formData)
                {
                    if (value is IFormFile file)
                    {
                        using var memoryStream = new MemoryStream();
                        file.CopyTo(memoryStream);

                        var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                        multipartContent.Add(byteArrayContent, file.Name, file.FileName);
                    }
                    else
                    {
                        if (value is IEnumerable values && value is not string)
                        {
                            foreach(var v in values)
                            {
                                multipartContent.Add(new StringContent(v.ToString() ?? string.Empty), key);
                            }
                        }
                        else
                        {
                            multipartContent.Add(new StringContent(value.ToString() ?? string.Empty), key);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Data must be a Dictionary<string, object> for multipart/form-data media type.");
            }
            return multipartContent;
        }
        else
        {
            throw new NotSupportedException($"Media type {mediaType} is not supported.");
        }
    }

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
