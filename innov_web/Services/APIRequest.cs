using System.Security.AccessControl;
using static innov_web.Const.SD;

namespace innov_web.Services
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
