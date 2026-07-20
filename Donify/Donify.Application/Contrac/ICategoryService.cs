using Donify.Application.DTOs;

namespace Donify.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> CreateAsync(CategoryDto dto);
        Task UpdateAsync(int id, CategoryDto dto);
        Task DeleteAsync(int id);
    }
}
