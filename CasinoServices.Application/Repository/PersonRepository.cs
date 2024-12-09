using CasinoServices.Application.Interfaces;
using CasinoServices.Domain.Entities;
using CasinoServices.Infrastracture.Services;
using MongoDB.Driver;

namespace CasinoServices.Application.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MongoDBService _dbService;
        private readonly IMongoCollection<Person> _personCollection;

        public PersonRepository(MongoDBService dbService)
        {
            _dbService = dbService;
            _personCollection = _dbService.GetPersonCollection();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personCollection.Find(person => true).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(string id)
        {
            return await _personCollection.Find(person => person.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Person person)
        {
            await _personCollection.InsertOneAsync(person);
        }

        public async Task UpdateAsync(Person person)
        {
            var filter = Builders<Person>.Filter.Eq(p => p.Id, person.Id);
            await _personCollection.ReplaceOneAsync(filter, person);
        }

        public async Task DeleteAsync(string id)
        {
            await _personCollection.DeleteOneAsync(person => person.Id == id);
        }

    }
}
