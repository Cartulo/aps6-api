using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aps6Api.Movimentacoes
{
    public class Movimentacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
        [BsonElement("Quantidade")]
        public int Quantidade { get; set; }
        [BsonElement("QuantidadeMinima")]
        public int QuantidadeMinima { get; set; }
        [BsonElement("DataEntrada")]
        public DateTime DataEntrada { get; set; }
        [BsonElement("DataSaida")]
        public DateTime DataSaida { get; set; }
    }
}