using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Project>> GetByStatusAsync(string status) =>
            await _dbSet.Where(p => p.Status == status).ToListAsync();

        public async Task<IEnumerable<Project>> GetByStaffIdAsync(int staffId) =>
            await _dbSet.Where(p => p.StaffId == staffId).ToListAsync();
    }
}