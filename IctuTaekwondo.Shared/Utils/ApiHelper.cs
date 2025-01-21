using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace IctuTaekwondo.Shared.Utils;
public class APIHelper
{
    private readonly HttpClient _httpClient;

    // Constructor
    public APIHelper(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    }

    /// <summary>
    /// Đặt mã thông báo ủy quyền
    /// </summary>
    public void SetAuthorizationToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    /// <summary>
    /// Gửi yêu cầu GET
    /// </summary>
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        var jsonResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(jsonResult);
    }

    /// <summary>
    /// Gửi yêu cầu POST
    /// </summary>
    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
        var jsonResult = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResult);
    }

    /// <summary>
    /// Gửi yêu cầu PUT
    /// </summary>
    public async Task<T?> PutAsync<T>(string endpoint, object data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(endpoint, content);
        var jsonResult = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResult);
    }

    /// <summary>
    /// Gửi yêu cầu DELETE
    /// </summary>
    public async Task<bool> DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);

        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Gửi yêu cầu POST với tệp (hình ảnh hoặc bất kỳ loại tệp nào).
    /// </summary>
    public async Task<T?> PostFileAsync<T>(string endpoint, string filePath, string fileKey = "file", object additionalData = null)
    {
        // Tạo đối tượng MultipartFormDataContent
        using (var multipartContent = new MultipartFormDataContent())
        {
            // Thêm tệp vào dữ liệu biểu mẫu
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            multipartContent.Add(fileContent, fileKey, Path.GetFileName(filePath));

            // Thêm bất kỳ dữ liệu bổ sung nào (tùy chọn)
            if (additionalData != null)
            {
                var jsonData = JsonConvert.SerializeObject(additionalData);
                multipartContent.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "metadata");
            }

            // Gửi yêu cầu POST
            var response = await _httpClient.PostAsync(endpoint, multipartContent);

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
    }
}
