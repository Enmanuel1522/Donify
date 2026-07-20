using Donify.Infrastructure.Context;
using Donify.Domain.Entities;
using Donify.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donify.Infrastructure.Repositories
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