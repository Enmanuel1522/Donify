using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class DonationService : IDonationService
    {
        private readonly IUnitOfWork _uow;

        public DonationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DonationDto?> GetByIdAsync(int id)
        {
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null) return null;

            return new DonationDto
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                Amount = donation.Amount,
                Description = donation.Description,
                DonatedAt = donation.DonatedAt,
                Status = donation.Status
            };
        }

        public async Task<IEnumerable<DonationDto>> GetAllAsync()
        {
            var donations = await _uow.Donations.GetAllAsync();
            return donations.Select(donation => new DonationDto
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                Amount = donation.Amount,
                Description = donation.Description,
                DonatedAt = donation.DonatedAt,
                Status = donation.Status
            });
        }

        public async Task<DonationDto> CreateAsync(DonationDto dto)
        {
            var donorExists = await _uow.Donors.GetByIdAsync(dto.DonorId);
            if (donorExists == null) throw new KeyNotFoundException($"No se encontró un donante con id {dto.DonorId}");

            var donation = new Donation
            {
                DonorId = dto.DonorId,
                Amount = dto.Amount,
                Description = dto.Description,
                DonatedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            await _uow.Donations.AddAsync(donation);
            await _uow.SaveAsync();

            dto.Id = donation.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, DonationDto dto)
        {
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null) throw new KeyNotFoundException($"No se encontró una donación con id {id}");

            donation.Amount = dto.Amount;
            donation.Description = dto.Description;
            donation.Status = dto.Status;

            await _uow.Donations.UpdateAsync(donation);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donation = await _uow.Donations.GetByIdAsync(id);
            if (donation == null) throw new KeyNotFoundException($"No se encontró una donación con id {id}");

            await _uow.Donations.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}
