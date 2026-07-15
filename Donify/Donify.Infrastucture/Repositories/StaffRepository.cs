using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DataContext context) : base(context) { }

        public async Task<Staff?> GetByEmailAsync(string email) =>
            await _dbSet.FirstOrDefaultAsync(s => s.Email == email);

        public async Task<IEnumerable<Staff>> GetActiveAsync() =>
            await _dbSet.Where(s => s.IsActive).ToListAsync();
    }
}