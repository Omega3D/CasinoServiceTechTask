using CasinoServices.Domain.Entities;
using MongoDB.Driver;

namespace CasinoServices.Infrastracture.Interfaces
{
    public interface IMongoDBService
    {
        IMongoCollection<T> GetCollection<T>(string collectionName) where T : class;
        IMongoCollection<Person> GetPersonCollection();
    }

}
