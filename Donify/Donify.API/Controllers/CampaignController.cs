using Donify.Application.DTOs;
using Donify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
        {
            var campaign = await _campaignService.GetByIdAsync(id);
            if (campaign == null)
                return NotFound($"No se encontró una campaña con id {id}");

            return Ok(campaign);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllAsync();
            return Ok(campaigns);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignDto>> CreateCampaign(CampaignDto dto)
        {
            var created = await _campaignService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCampaign), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, CampaignDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            await _campaignService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            await _campaignService.DeleteAsync(id);
            return NoContent();
        }
    }
}