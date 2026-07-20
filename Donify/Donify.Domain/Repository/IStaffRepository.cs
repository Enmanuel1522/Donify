using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff?> GetByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetActiveAsync();
    }
}