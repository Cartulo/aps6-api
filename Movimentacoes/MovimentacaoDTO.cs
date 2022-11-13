namespace Aps6Api.Movimentacoes
{
    public class MovimentacaoDTO
    {
        public string? Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeMinima { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}