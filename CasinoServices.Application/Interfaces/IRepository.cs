using CasinoServices.Domain.Entities;

namespace CasinoServices.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task CreateAsync(T entity);
        Task DeleteAsync(string id);
    }
}
