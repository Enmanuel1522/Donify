using Donify.Domain.Entities;

namespace Donify.Domain.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);
    }
}
