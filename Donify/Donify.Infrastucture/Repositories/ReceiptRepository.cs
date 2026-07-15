using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.API.Repositories
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(DataContext context) : base(context) { }

        public async Task<Receipt?> GetByDonationIdAsync(int donationId) =>
            await _dbSet.FirstOrDefaultAsync(r => r.DonationId == donationId);

        public async Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber) =>
            await _dbSet.FirstOrDefaultAsync(r => r.ReceiptNumber == receiptNumber);
    }
}