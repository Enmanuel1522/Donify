using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IDonorRepository : IGenericRepository<Donor>
    {
        Task<Donor?> GetByEmailAsync(string email);
        Task<IEnumerable<Donor>> GetActiveDonorsAsync();
    }
}