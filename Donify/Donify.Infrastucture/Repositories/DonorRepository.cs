using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace Donify.Infrastructure.Repositories
{
    public class DonorRepository : GenericRepository<Donor>, IDonorRepository
    {
        public DonorRepository(DataContext context) : base(context) { }

        public async Task<Donor?> GetByEmailAsync(string email) =>
            await _dbSet.FirstOrDefaultAsync(d => d.Email == email);

        public async Task<IEnumerable<Donor>> GetActiveDonorsAsync() =>
            await _dbSet.Where(d => d.IsActive).ToListAsync();
    }
}