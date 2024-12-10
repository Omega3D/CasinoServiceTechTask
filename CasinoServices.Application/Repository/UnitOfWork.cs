using CasinoServices.Application.Interfaces;
using CasinoServices.Infrastracture.Services;
using MongoDB.Driver;

namespace CasinoServices.Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDBService _mongoDBService;

        public UnitOfWork(MongoDBService dBService)
        {
            _mongoDBService = dBService;
            Person = new PersonRepository(_mongoDBService, _mongoDBService.GetPersonCollection());
        }

        public IPersonRepository Person { get; private set; }
    }
}
