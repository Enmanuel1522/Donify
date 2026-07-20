using Donify.Application.DTOs;
using Donify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorDto>> GetDonor(int id)
        {
            var donor = await _donorService.GetByIdAsync(id);
            if (donor == null)
                return NotFound($"No se encontró un donante con id {id}");

            return Ok(donor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorDto>>> GetAllDonors()
        {
            var donors = await _donorService.GetAllAsync();
            return Ok(donors);
        }

        [HttpPost]
        public async Task<ActionResult<DonorDto>> CreateDonor(DonorDto dto)
        {
            var created = await _donorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetDonor), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, DonorDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            await _donorService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            await _donorService.DeleteAsync(id);
            return NoContent();
        }
    }
}