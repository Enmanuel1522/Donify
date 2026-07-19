using Donify.API.DTOs;


namespace Donify.Application.Interfaces
{
    public interface ICampaignService
    {
        Task<CampaignDto?> GetByIdAsync(int id);
        Task<IEnumerable<CampaignDto>> GetAllAsync();
        Task<CampaignDto> CreateAsync(CampaignDto dto);
        Task UpdateAsync(int id, CampaignDto dto);
        Task DeleteAsync(int id);
    }
}
