using Donify.API.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IStaffService
    {
        Task<StaffDto?> GetByIdAsync(int id);
        Task<IEnumerable<StaffDto>> GetAllAsync();
        Task<StaffDto> CreateAsync(StaffDto dto);
        Task UpdateAsync(int id, StaffDto dto);
        Task DeleteAsync(int id);
    }
}
