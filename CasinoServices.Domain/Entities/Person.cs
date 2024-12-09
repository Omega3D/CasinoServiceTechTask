using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CasinoServices.Domain.Entities
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;
        [BsonElement("address")]
        public string? Address {  get; set; }
    }
}
