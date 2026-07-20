using Donify.Application.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto> CreateAsync(ProjectDto dto);
        Task UpdateAsync(int id, ProjectDto dto);
        Task DeleteAsync(int id);
    }
}
