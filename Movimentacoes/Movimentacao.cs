using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aps6Api.Movimentacoes
{
    public class Movimentacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Quantidade")]
        public int Quantidade { get; set; }

        [BsonElement("ProdutoId")]
        public string ProdutoId { get; set; } = null!;

        [BsonElement("SetorEntradaId")]
        public string? SetorEntradaId { get; set; } = null!;

        [BsonElement("SetorSaidaId")]
        public string? SetorSaidaId { get; set; } = null!;
    }
}