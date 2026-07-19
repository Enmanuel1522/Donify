using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CampaignController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
        {
            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null)
                return NotFound($"No se encontró una campaña con id {id}");

            var dto = new CampaignDto
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
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignDto>> CreateCampaign(CampaignDto dto)
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
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, CampaignDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null)
                return NotFound($"No se encontró una campaña con id {id}");

            campaign.Name = dto.Name;
            campaign.Description = dto.Description;
            campaign.GoalAmount = dto.GoalAmount;
            campaign.StartDate = dto.StartDate;
            campaign.EndDate = dto.EndDate;
            campaign.CategoryId = dto.CategoryId;

            await _uow.Campaigns.UpdateAsync(campaign);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var campaign = await _uow.Campaigns.GetByIdAsync(id);
            if (campaign == null)
                return NotFound($"No se encontró una campaña con id {id}");

            await _uow.Campaigns.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}