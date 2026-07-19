using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DonationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DonationController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonationDto>> GetDonation(int id)
        {
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null)
            {
                return NotFound($"No se encontró una donación con id {id}");
            }
            var dto = new DonationDto
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                Amount = donation.Amount,
                Description = donation.Description,
                DonatedAt = donation.DonatedAt,
                Status = donation.Status
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<DonationDto>> CreateDonation(DonationDto dto)
        {
            var donorExists = await _uow.Donors.GetByIdAsync(dto.DonorId);
            if (donorExists == null)
            {
                return NotFound($"No se encontró un donante con id {dto.DonorId}");
            }

            var donation = new Donation
            {
                DonorId = dto.DonorId,
                Amount = dto.Amount,
                Description = dto.Description,
                DonatedAt = DateTime.UtcNow,
                Status = "Pending",
                Type = "General",
                PaymentMethod = "Unknown"
            };
            await _uow.Donations.AddAsync(donation);
            await _uow.SaveAsync();
            dto.Id = donation.Id;
            return CreatedAtAction(nameof(GetDonation), new { id = donation.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonation(int id, DonationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El id de la ruta no coincide con el del cuerpo");
            }
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null)
            {
                return NotFound($"No se encontró una donación con id {id}");
            }
            donation.Amount = dto.Amount;
            donation.Description = dto.Description;
            donation.Status = dto.Status;
            await _uow.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null)
            {
                return NotFound($"No se encontró una donación con id {id}");
            }
            await _uow.Donations.DeleteAsync(donation.Id);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}
