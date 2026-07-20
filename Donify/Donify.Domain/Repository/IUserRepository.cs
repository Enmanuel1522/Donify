using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetActiveAsync();
    }
}