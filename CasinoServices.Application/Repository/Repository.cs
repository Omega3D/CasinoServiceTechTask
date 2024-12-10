using CasinoServices.Application.Interfaces;
using CasinoServices.Domain.Entities;
using CasinoServices.Infrastracture.Interfaces;
using CasinoServices.Infrastracture.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CasinoServices.Application.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly IMongoCollection<T> _collection;
        private readonly IMongoDBService _mongoDBService;

        public Repository(IMongoDBService dBService, IMongoCollection<T> collection)
        {
            _mongoDBService = dBService;
            _collection = collection;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("Invalid id format");

            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}
