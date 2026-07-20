using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
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