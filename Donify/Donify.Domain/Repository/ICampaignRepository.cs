using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<IEnumerable<Campaign>> GetActiveAsync();
        Task<IEnumerable<Campaign>> GetByCategoryIdAsync(int categoryId);
    }
}