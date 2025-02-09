using System.Collections.Generic;
using System.Net;

namespace IctuTaekwondo.Winforms.Responses
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        public T Data { get; set; }
    }
}
