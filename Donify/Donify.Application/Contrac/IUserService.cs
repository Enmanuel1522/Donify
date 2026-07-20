using Donify.Application.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(UserDto dto);
        Task UpdateAsync(int id, UserDto dto);
        Task DeleteAsync(int id);
    }
}
