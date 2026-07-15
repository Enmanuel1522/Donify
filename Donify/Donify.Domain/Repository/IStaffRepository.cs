using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff?> GetByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetActiveAsync();
    }
}