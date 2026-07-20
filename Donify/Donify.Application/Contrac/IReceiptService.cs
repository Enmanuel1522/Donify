using Donify.Application.DTOs;

namespace Donify.Application.Interfaces
{
    public interface IReceiptService
    {
        Task<ReceiptDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReceiptDto>> GetAllAsync();
        Task<ReceiptDto> CreateAsync(ReceiptDto dto);
        Task UpdateAsync(int id, ReceiptDto dto);
        Task DeleteAsync(int id);
    }
}
