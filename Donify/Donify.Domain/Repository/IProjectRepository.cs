using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetByStatusAsync(string status);
        Task<IEnumerable<Project>> GetByStaffIdAsync(int staffId);
    }
}
