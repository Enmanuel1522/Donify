using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IDonationRepository : IGenericRepository<Donation>
    {
        Task<IEnumerable<Donation>> GetByDonorIdAsync(int donorId);
        Task<IEnumerable<Donation>> GetByStatusAsync(string status);
    }
}
