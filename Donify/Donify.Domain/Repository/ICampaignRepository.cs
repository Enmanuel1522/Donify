using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<IEnumerable<Campaign>> GetActiveAsync();
        Task<IEnumerable<Campaign>> GetByCategoryIdAsync(int categoryId);
    }
}