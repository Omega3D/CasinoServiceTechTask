using CasinoServices.Domain.Entities;

namespace CasinoServices.Application.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task UpdateAsync(Person person);
    }
}
