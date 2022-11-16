using System.Net;

namespace ComplainManagment.Models
{
    public class ResponseApi<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Response { get; set; }
    }
}
 