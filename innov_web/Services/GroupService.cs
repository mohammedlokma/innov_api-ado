using innov_web.Const;
using innov_web.Models.DTO;
using innov_web.Services.Interfaces;

namespace innov_web.Services
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string groupUrl;
        public GroupService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            groupUrl = configuration.GetValue<string>("ServiceUrls:InnovAPI");
        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = groupUrl + "/api/Group/GetGroups",
            });

        }
        public Task<T> CreateAsync<T>(GroupDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = groupUrl + "/api/Group/CreateGroup"

            });
        }
        public Task<T> GetGroupVerbsAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = groupUrl + "/api/Group/GetGroupVerbs?groupId=" + id,
            });

        }
    }
}
