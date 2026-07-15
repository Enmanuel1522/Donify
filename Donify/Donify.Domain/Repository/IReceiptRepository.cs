using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<Receipt?> GetByDonationIdAsync(int donationId);
        Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber);
    }
}