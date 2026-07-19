using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StaffController : ControllerBase
    {

        private readonly IUnitOfWork _uow;

        public StaffController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaff(int id)
        {
            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null)
                return NotFound($"No se encontró un staff con id {id}");

            var dto = new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Position = staff.Position,
                Email = staff.Email,
                Phone = staff.Phone,
                HiredAt = staff.HiredAt,
                IsActive = staff.IsActive
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff(StaffDto dto)
        {
            var staff = new Staff
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Position = dto.Position,
                Email = dto.Email,
                Phone = dto.Phone,
                HiredAt = dto.HiredAt,
                IsActive = true
            };

            await _uow.Staffs.AddAsync(staff);
            await _uow.SaveAsync();

            dto.Id = staff.Id;
            return CreatedAtAction(nameof(GetStaff), new { id = staff.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, StaffDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null)
                return NotFound($"No se encontró un staff con id {id}");

            staff.FirstName = dto.FirstName;
            staff.LastName = dto.LastName;
            staff.Position = dto.Position;
            staff.Email = dto.Email;
            staff.Phone = dto.Phone;
            staff.HiredAt = dto.HiredAt;
            staff.IsActive = dto.IsActive;

            await _uow.Staffs.UpdateAsync(staff);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null)
                return NotFound($"No se encontró un staff con id {id}");

            await _uow.Staffs.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}