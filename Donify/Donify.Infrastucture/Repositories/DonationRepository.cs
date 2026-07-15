using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
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