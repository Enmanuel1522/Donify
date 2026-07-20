using Donify.Application.DTOs;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task<UserDto> CreateAsync(UserDto dto)
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
            return dto;
        }

        public async Task UpdateAsync(int id, UserDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException($"No se encontró un usuario con id {id}");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException($"No se encontró un usuario con id {id}");

            await _uow.Users.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}
