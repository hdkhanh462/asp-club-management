using System.Net;

namespace IctuTaekwondo.Shared.Responses
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = [];
        public T? Data { get; set; }
    }
}
