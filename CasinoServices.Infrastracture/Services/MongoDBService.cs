using CasinoServices.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace CasinoServices.Infrastracture.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Person> _personCollection;

        public MongoDBService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDB:ConnectionURI").Value;
            var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;
            var collectionName = configuration.GetSection("MongoDB:CollectionName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _personCollection = database.GetCollection<Person>(collectionName);
        }

        public IMongoCollection<Person> GetPersonCollection()
        {
            return _personCollection;
        }
    }
}
