using Donify.API.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IDonationService
    {
        Task<DonationDto?> GetByIdAsync(int id);
        Task<IEnumerable<DonationDto>> GetAllAsync();
        Task<DonationDto> CreateAsync(DonationDto dto);
        Task UpdateAsync(int id, DonationDto dto);
        Task DeleteAsync(int id);
    }
}
