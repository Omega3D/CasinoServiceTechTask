using CasinoServices.Application.Repository;
using CasinoServices.Domain.Entities;
using CasinoServices.Infrastracture.Interfaces;
using MongoDB.Driver;
using Moq;

namespace CasinoServices.Tests
{
    public class UnitOfWorkTests
    {
        private Mock<IMongoDBService> _mockMongoService;
        private Mock<IMongoCollection<Person>> _mockPersonCollection;
        private UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            _mockMongoService = new Mock<IMongoDBService>();
            _mockPersonCollection = new Mock<IMongoCollection<Person>>();

            _mockMongoService
                .Setup(mongo => mongo.GetPersonCollection())
                .Returns(_mockPersonCollection.Object);

            _unitOfWork = new UnitOfWork(_mockMongoService.Object);
        }

        [Fact]
        public void UnitOfWork_InitializesPersonRepository()
        {
            var personRepository = _unitOfWork.Person;

            Assert.NotNull(personRepository);
            Assert.IsType<PersonRepository>(personRepository);
        }

        [Fact]
        public async Task Repository_GetAllAsync_ReturnsNonEmptyCollection()
        {
            var mockPersons = new List<Person>
            {
                new Person { Id = "1424252", Name = "John Doe", Email = "john.doe@example.com", Phone = "123456789", Address = "123 Main St" },
                new Person { Id = "2421313", Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "987654321", Address = "456 Elm St" }
            };

            var mockCursor = new Mock<IAsyncCursor<Person>>();
            mockCursor.Setup(x => x.Current).Returns(mockPersons);

            var mockPersonCollection = new Mock<IMongoCollection<Person>>();
            mockPersonCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<Person>>(),
                    It.IsAny<FindOptions<Person, Person>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(mockCursor.Object);

            var mockMongoService = new Mock<IMongoDBService>();
            mockMongoService.Setup(x => x.GetPersonCollection())
                .Returns(mockPersonCollection.Object);

            var repository = new Repository<Person>(mockMongoService.Object, mockPersonCollection.Object);

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<Person>>(result);
            Assert.Equal(2, result.Count());
            Assert.Collection(result,
                item => Assert.Equal("John Doe", item.Name),
                item => Assert.Equal("Jane Doe", item.Name));
        }
    }
}
