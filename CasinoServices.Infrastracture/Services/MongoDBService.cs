using CasinoServices.Domain.Entities;
using CasinoServices.Infrastracture.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace CasinoServices.Infrastracture.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDB:ConnectionURI").Value
                ?? throw new ArgumentNullException("MongoDB connection string is not configured");

            var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value
                ?? throw new ArgumentNullException("MongoDB database name is not configured");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : class
        {
            return _database.GetCollection<T>(collectionName);
        }

        public virtual IMongoCollection<Person> GetPersonCollection()
        {
            return GetCollection<Person>("Persons");
        }
    }
}