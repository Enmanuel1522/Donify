using Donify.Application.DTOs;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _uow;

        public StaffService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StaffDto?> GetByIdAsync(int id)
        {
            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null) return null;

            return new StaffDto
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
        }

        public async Task<IEnumerable<StaffDto>> GetAllAsync()
        {
            var staffs = await _uow.Staffs.GetAllAsync();
            return staffs.Select(staff => new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Position = staff.Position,
                Email = staff.Email,
                Phone = staff.Phone,
                HiredAt = staff.HiredAt,
                IsActive = staff.IsActive
            });
        }

        public async Task<StaffDto> CreateAsync(StaffDto dto)
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
            return dto;
        }

        public async Task UpdateAsync(int id, StaffDto dto)
        {
            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null) throw new KeyNotFoundException($"No se encontró un staff con id {id}");

            staff.FirstName = dto.FirstName;
            staff.LastName = dto.LastName;
            staff.Position = dto.Position;
            staff.Email = dto.Email;
            staff.Phone = dto.Phone;
            staff.HiredAt = dto.HiredAt;
            staff.IsActive = dto.IsActive;

            await _uow.Staffs.UpdateAsync(staff);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _uow.Staffs.GetByIdAsync(id);
            if (staff == null) throw new KeyNotFoundException($"No se encontró un staff con id {id}");

            await _uow.Staffs.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}