using Donify.Application.DTOs;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork _uow;

        public DonorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DonorDto?> GetByIdAsync(int id)
        {
            var donor = await _uow.Donors.GetByIdAsync(id);
            if (donor == null) return null;

            return new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Email = donor.Email
            };
        }

        public async Task<IEnumerable<DonorDto>> GetAllAsync()
        {
            var donors = await _uow.Donors.GetAllAsync();
            return donors.Select(donor => new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Email = donor.Email
            });
        }

        public async Task<DonorDto> CreateAsync(DonorDto dto)
        {
            var donor = new Donor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                RegisteredAt = DateTime.UtcNow,
                IsActive = true
            };

            await _uow.Donors.AddAsync(donor);
            await _uow.SaveAsync();

            dto.Id = donor.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, DonorDto dto)
        {
            var donor = await _uow.Donors.GetByIdAsync(id);
            if (donor == null) throw new KeyNotFoundException($"No se encontró un donante con id {id}");

            donor.FirstName = dto.FirstName;
            donor.LastName = dto.LastName;
            donor.Email = dto.Email;

            await _uow.Donors.UpdateAsync(donor);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donor = await _uow.Donors.GetByIdAsync(id);
            if (donor == null) throw new KeyNotFoundException($"No se encontró un donante con id {id}");

            await _uow.Donors.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}