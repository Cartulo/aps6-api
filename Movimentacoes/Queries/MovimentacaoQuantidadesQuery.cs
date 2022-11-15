namespace Aps6Api.Movimentacoes.Queries
{
    public class MovimentacaoQuantidadesQuery
    {
        public List<Movimentacao> Movimentacoes { get; set; }
        public string SetorId { get; set; }
        public int QuantidadeEntrada { get; set; }
        public int QuantidadeSaida { get; set; }
        public int QuantidadeTotal { get; set; }

        public MovimentacaoQuantidadesQuery(List<Movimentacao> movimentacoes, string setorId, int quantidadeEntrada, int quantidadeSaida, int quantidadeTotal)
        {
            SetorId = setorId;
            Movimentacoes = movimentacoes;
            QuantidadeEntrada = quantidadeEntrada;
            QuantidadeSaida = quantidadeSaida;
            QuantidadeTotal = quantidadeTotal;
        }
    }
}