using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IDonorRepository : IGenericRepository<Donor>
    {
        Task<Donor?> GetByEmailAsync(string email);
        Task<IEnumerable<Donor>> GetActiveDonorsAsync();
    }
}