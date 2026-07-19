using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound($"No se encontró un usuario con id {id}");

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = string.Empty,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveAsync();

            dto.Id = user.Id;
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound($"No se encontró un usuario con id {id}");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound($"No se encontró un usuario con id {id}");

            await _uow.Users.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
