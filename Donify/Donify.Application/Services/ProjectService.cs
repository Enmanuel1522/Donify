using Donify.Application.DTOs;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _uow;

        public ProjectService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                RequiredBudget = project.RequiredBudget,
                AssignedBudget = project.AssignedBudget,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                StaffId = project.StaffId
            };
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _uow.Projects.GetAllAsync();
            return projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                RequiredBudget = project.RequiredBudget,
                AssignedBudget = project.AssignedBudget,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                StaffId = project.StaffId
            });
        }

        public async Task<ProjectDto> CreateAsync(ProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                RequiredBudget = dto.RequiredBudget,
                AssignedBudget = dto.AssignedBudget,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                StaffId = dto.StaffId
            };

            await _uow.Projects.AddAsync(project);
            await _uow.SaveAsync();

            dto.Id = project.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, ProjectDto dto)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null) throw new KeyNotFoundException($"No se encontró un proyecto con id {id}");

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.RequiredBudget = dto.RequiredBudget;
            project.AssignedBudget = dto.AssignedBudget;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Status = dto.Status;
            project.StaffId = dto.StaffId;

            await _uow.Projects.UpdateAsync(project);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null) throw new KeyNotFoundException($"No se encontró un proyecto con id {id}");

            await _uow.Projects.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}
