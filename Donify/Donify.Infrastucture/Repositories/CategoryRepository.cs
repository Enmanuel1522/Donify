using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}