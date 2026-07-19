using Donify.API.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IDonorService
    {
        Task<DonorDto?> GetByIdAsync(int id);
        Task<IEnumerable<DonorDto>> GetAllAsync();
        Task<DonorDto> CreateAsync(DonorDto dto);
        Task UpdateAsync(int id, DonorDto dto);
        Task DeleteAsync(int id);
    }
}
