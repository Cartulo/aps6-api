using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aps6Api.Setores
{
    public class Setor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
}