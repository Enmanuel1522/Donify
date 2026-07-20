using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<Receipt?> GetByDonationIdAsync(int donationId);
        Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber);
    }
}