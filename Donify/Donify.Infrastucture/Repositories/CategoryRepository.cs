using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}