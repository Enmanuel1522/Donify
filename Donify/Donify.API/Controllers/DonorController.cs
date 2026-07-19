using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DonorController : ControllerBase
    {
        public IUnitOfWork _uow;


        public DonorController(IUnitOfWork uow)
        {
            _uow = uow;

        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<DonorDto>> GetDonor(int id)
        {
            var donor = await _uow.Donors.GetByIdAsync(id);

            if (donor == null) 
            { 
                return NotFound($"No se encontro un donante con id {id}");
            }
            var dto = new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastNeme = donor.LastNeme,
                Email = donor.Email
            };

            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<ActionResult<DonorDto>> CreateDonor(DonorDto dto)
        {
            var donor = new Donor
            {
                FirstName = dto.FirstName,
                LastNeme = dto.LastNeme,
                Email = dto.Email,
                DonorType = "Individual",
                RegisteredAt = DateTime.UtcNow,
                IsActive = true
            };

            await _uow.Donors.AddAsync(donor);
            await _uow.SaveAsync();

            dto.Id = donor.Id;

            return CreatedAtAction(nameof(GetDonor), new { id = donor.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, DonorDto dto)
        {
            if (id != dto.Id) 
            { 
                return BadRequest("El id de la ruta no coincide con el del cuerpo");
            }
            var donor = await _uow.Donors.GetByIdAsync(id);
            if (donor == null) 
            { 
                return NotFound($"No se encontró un donante con id {id}");
            }
            donor.FirstName = dto.FirstName;
            donor.LastNeme = dto.LastNeme;
            donor.Email = dto.Email;

            await _uow.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var donor = await _uow.Donors.GetByIdAsync(id);
            if (donor == null) 
            { 
                return NotFound($"No se encontró un donante con id {id}");
            }
            await _uow.Donors.DeleteAsync(donor.Id);
            await _uow.SaveAsync();

            return NoContent();
        }

    }
}



