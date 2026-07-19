using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Donify.Application.Interfaces;

namespace Donify.Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IUnitOfWork _uow;

        public ReceiptService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ReceiptDto?> GetByIdAsync(int id)
        {
            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null) return null;

            return new ReceiptDto
            {
                Id = receipt.Id,
                DonationId = receipt.DonationId,
                ReceiptNumber = receipt.ReceiptNumber,
                IssuedAt = receipt.IssuedAt,
                SentByEmail = receipt.SentByEmail
            };
        }

        public async Task<IEnumerable<ReceiptDto>> GetAllAsync()
        {
            var receipts = await _uow.Receipts.GetAllAsync();
            return receipts.Select(receipt => new ReceiptDto
            {
                Id = receipt.Id,
                DonationId = receipt.DonationId,
                ReceiptNumber = receipt.ReceiptNumber,
                IssuedAt = receipt.IssuedAt,
                SentByEmail = receipt.SentByEmail
            });
        }

        public async Task<ReceiptDto> CreateAsync(ReceiptDto dto)
        {
            var donationExists = await _uow.Donations.GetByIdAsync(dto.DonationId);
            if (donationExists == null) throw new KeyNotFoundException($"No se encontró una donación con id {dto.DonationId}");

            var receipt = new Receipt
            {
                DonationId = dto.DonationId,
                ReceiptNumber = dto.ReceiptNumber,
                IssuedAt = DateTime.UtcNow,
                SentByEmail = false
            };

            await _uow.Receipts.AddAsync(receipt);
            await _uow.SaveAsync();

            dto.Id = receipt.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, ReceiptDto dto)
        {
            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null) throw new KeyNotFoundException($"No se encontró un recibo con id {id}");

            receipt.ReceiptNumber = dto.ReceiptNumber;
            receipt.SentByEmail = dto.SentByEmail;

            await _uow.Receipts.UpdateAsync(receipt);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null) throw new KeyNotFoundException($"No se encontró un recibo con id {id}");

            await _uow.Receipts.DeleteAsync(id);
            await _uow.SaveAsync();
        }
    }
}
