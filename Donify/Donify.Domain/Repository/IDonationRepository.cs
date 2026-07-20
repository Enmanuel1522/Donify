using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IDonationRepository : IGenericRepository<Donation>
    {
        Task<IEnumerable<Donation>> GetByDonorIdAsync(int donorId);
        Task<IEnumerable<Donation>> GetByStatusAsync(string status);
    }
}
