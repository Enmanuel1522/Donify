using Donify.Application.DTOs;
using Donify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaff(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            if (staff == null)
                return NotFound($"No se encontró un staff con id {id}");

            return Ok(staff);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAllStaff()
        {
            var staffs = await _staffService.GetAllAsync();
            return Ok(staffs);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff(StaffDto dto)
        {
            var created = await _staffService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetStaff), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, StaffDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            await _staffService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffService.DeleteAsync(id);
            return NoContent();
        }
    }
}