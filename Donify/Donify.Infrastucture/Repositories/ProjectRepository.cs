using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
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