using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _uow;

        public CampaignService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CampaignDto?> GetByIdAsync(int id)
        {
            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null) return null;

            return new CampaignDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                GoalAmount = campaign.GoalAmount,
                CollectedAmount = campaign.CollectedAmount,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Status = campaign.Status.ToString(),
                CategoryId = campaign.CategoryId
            };
        }

        public async Task<IEnumerable<CampaignDto>> GetAllAsync()
        {
            var campaigns = await _uow.Campaigns.GetAllAsync();
            return campaigns.Select(campaign => new CampaignDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                GoalAmount = campaign.GoalAmount,
                CollectedAmount = campaign.CollectedAmount,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Status = campaign.Status.ToString(),
                CategoryId = campaign.CategoryId
            });
        }

        public async Task<CampaignDto> CreateAsync(CampaignDto dto)
        {
            var campaign = new Campaign
            {
                Name = dto.Name,
                Description = dto.Description,
                GoalAmount = dto.GoalAmount,
                CollectedAmount = 0,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = CampaignStatus.Active,
                CategoryId = dto.CategoryId
            };

            await _uow.Campaigns.AddAsync(campaign);
            await _uow.SaveAsync();

            dto.Id = campaign.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, CampaignDto dto)
        {
            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null) throw new KeyNotFoundException($"No se encontró una campaña con id {id}");

            campaign.Name = dto.Name;
            campaign.Description = dto.Description;
            campaign.GoalAmount = dto.GoalAmount;
            campaign.StartDate = dto.StartDate;
            campaign.EndDate = dto.EndDate;
            campaign.CategoryId = dto.CategoryId;

            await _uow.Campaigns.UpdateAsync(campaign);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null) throw new KeyNotFoundException($"No se encontró una campaña con id {id}");

            await _uow.Campaigns.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}
