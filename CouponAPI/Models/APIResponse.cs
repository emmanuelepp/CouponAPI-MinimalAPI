using System.Net;

namespace CouponAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrosMessages = new List<string>();
        }
        public bool Success { get; set; }
        public object? Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string>? ErrosMessages { get; set; }
    }
}
