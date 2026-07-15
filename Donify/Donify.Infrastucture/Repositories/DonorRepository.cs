using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace Donify.API.Repositories
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