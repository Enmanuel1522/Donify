using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
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