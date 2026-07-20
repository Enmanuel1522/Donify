using Donify.Application.DTOs;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _uow.Categories.GetAllAsync();
            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _uow.Categories.AddAsync(category);
            await _uow.SaveAsync();

            dto.Id = category.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, CategoryDto dto)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"No se encontró una categoría con id {id}");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _uow.Categories.UpdateAsync(category);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"No se encontró una categoría con id {id}");

            await _uow.Categories.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}