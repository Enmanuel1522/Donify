using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Campaign>> GetActiveAsync() =>
            await _dbSet.Where(c => c.Status == CampaignStatus.Active).ToListAsync();

        public async Task<IEnumerable<Campaign>> GetByCategoryIdAsync(int categoryId) =>
            await _dbSet.Where(c => c.CategoryId == categoryId).ToListAsync();
    }
}