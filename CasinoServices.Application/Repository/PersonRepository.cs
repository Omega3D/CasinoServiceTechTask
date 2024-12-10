using CasinoServices.Application.Interfaces;
using CasinoServices.Domain.Entities;
using CasinoServices.Infrastracture.Interfaces;
using CasinoServices.Infrastracture.Services;
using MongoDB.Driver;

namespace CasinoServices.Application.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDBService dBService, IMongoCollection<Person> collection)
            : base(dBService, collection)
        {
            
        }

        public async Task UpdateAsync(Person person)
        {
            var filter = Builders<Person>.Filter.Eq(p => p.Id, person.Id);
            await _collection.ReplaceOneAsync(filter, person);
        }
    }
}
