using Donify.Application.DTOs;
using Donify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonationDto>> GetDonation(int id)
        {
            var donation = await _donationService.GetByIdAsync(id);
            if (donation == null)
                return NotFound($"No se encontró una donación con id {id}");

            return Ok(donation);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonationDto>>> GetAllDonations()
        {
            var donations = await _donationService.GetAllAsync();
            return Ok(donations);
        }

        [HttpPost]
        public async Task<ActionResult<DonationDto>> CreateDonation(DonationDto dto)
        {
            var created = await _donationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetDonation), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonation(int id, DonationDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            await _donationService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            await _donationService.DeleteAsync(id);
            return NoContent();
        }
    }
}