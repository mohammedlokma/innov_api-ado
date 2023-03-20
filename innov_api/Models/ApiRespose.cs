using System.Net;

namespace task_api.Models
{
    public class ApiRespose
    {
        public ApiRespose()
        {
            ErrorMessages = new List<string>();

        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
