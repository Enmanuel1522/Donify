using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
{
    public class DonationRepository : GenericRepository<Donation>, IDonationRepository
    {
        public DonationRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Donation>> GetByDonorIdAsync(int donorId) =>
            await _dbSet.Where(d => d.DonorId == donorId).ToListAsync();

        public async Task<IEnumerable<Donation>> GetByStatusAsync(string status) =>
            await _dbSet.Where(d => d.Status == status).ToListAsync();
    }
}