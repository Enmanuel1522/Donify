using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<User>> GetActiveAsync() =>
            await _dbSet.Where(u => u.IsActive).ToListAsync();
    }
}