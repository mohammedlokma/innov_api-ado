using innov_web.Const;
using innov_web.Models.DTO;
using innov_web.Services.Interfaces;

namespace innov_web.Services
{
    public class VerbService : BaseService, IVerbService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string verbUrl;
        public VerbService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            verbUrl = configuration.GetValue<string>("ServiceUrls:InnovAPI");

        }
        
       
        public Task<T> CreateAsync<T>(VerbCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = verbUrl + "/api/Verb/CreateVerb"

            });
        }
        
            public Task<T> GetParamtersAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = verbUrl + "/api/TestEndPoint/GetParams?verbId=" + id,
            });

        }
        public Task<T> GetVerbAsync<T>(int verbId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = verbUrl + "/api/Verb/GetVerb?verbId=" + verbId,
            });

        }
        public Task<T> GetTablesNamesAsync<T>(int groupId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = verbUrl + "/api/Verb/GetTables?groupId=" + groupId,
            });

        }

    }
}
