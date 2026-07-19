using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound($"No se encontró una categoría con id {id}");

            var dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _uow.Categories.AddAsync(category);
            await _uow.SaveAsync();

            dto.Id = category.Id;
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound($"No se encontró una categoría con id {id}");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _uow.Categories.UpdateAsync(category);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound($"No se encontró una categoría con id {id}");

            await _uow.Categories.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
