using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetByStatusAsync(string status);
        Task<IEnumerable<Project>> GetByStaffIdAsync(int staffId);
    }
}
