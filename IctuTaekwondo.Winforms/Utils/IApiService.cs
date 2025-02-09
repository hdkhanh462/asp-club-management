using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Winforms.Responses;

namespace IctuTaekwondo.Winforms.Utils
{
    public interface IApiService
    {
        Task<ApiResponse<T>> GetAsync<T>(string requestUri);

        Task<ApiResponse<T>> PostAsync<T>(string requestUri, HttpContent content);

        Task<ApiResponse<T>> PutAsync<T>(string requestUri, HttpContent content);

        Task<ApiResponse<T>> DeleteAsync<T>(string requestUri);

        Task<ApiResponse<T>> PostMultipartAsync<T>(string requestUri, MultipartFormDataContent content);

        Task<ApiResponse<T>> PutMultipartAsync<T>(string requestUri, MultipartFormDataContent content);

        void SetAuthorizationHeader(string parameter, string scheme = "Bearer");

        void SetDefaultRequestHeader(string name, string value);

        void AddHeaders(Dictionary<string, string> headers);
    }
}
