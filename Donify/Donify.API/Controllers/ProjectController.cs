using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProjectController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null)
                return NotFound($"No se encontró un proyecto con id {id}");

            var dto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                RequiredBudget = project.RequiredBudget,
                AssignedBudget = project.AssignedBudget,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                StaffId = project.StaffId
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                RequiredBudget = dto.RequiredBudget,
                AssignedBudget = dto.AssignedBudget,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                StaffId = dto.StaffId
            };

            await _uow.Projects.AddAsync(project);
            await _uow.SaveAsync();

            dto.Id = project.Id;
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null)
                return NotFound($"No se encontró un proyecto con id {id}");

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.RequiredBudget = dto.RequiredBudget;
            project.AssignedBudget = dto.AssignedBudget;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Status = dto.Status;
            project.StaffId = dto.StaffId;

            await _uow.Projects.UpdateAsync(project);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null)
                return NotFound($"No se encontró un proyecto con id {id}");

            await _uow.Projects.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
