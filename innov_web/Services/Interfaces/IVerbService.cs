using innov_web.Models.DTO;

namespace innov_web.Services.Interfaces
{
    public interface IVerbService
    {
        Task<T> CreateAsync<T>(VerbCreateDto dto);
        Task<T> GetParamtersAsync<T>(int id);
        Task<T> GetTablesNamesAsync<T>(int groupId);
        Task<T> GetVerbAsync<T>(int verbId);
    }
}
