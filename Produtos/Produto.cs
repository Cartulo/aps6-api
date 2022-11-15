using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aps6Api.Produtos
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;

        [BsonElement("Setor")]
        public string[]? SetoresId { get; set; }
    }
}