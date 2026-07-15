using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
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