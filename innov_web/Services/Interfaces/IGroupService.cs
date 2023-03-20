using innov_web.Models.DTO;

namespace innov_web.Services.Interfaces
{
    public interface IGroupService
    {
        Task<T> CreateAsync<T>(GroupDto dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetGroupVerbsAsync<T>(int id);
    }
}
