namespace Aps6Api.Movimentacoes.Queries
{
    public class MovimentacaoProdutoQuantidadesQuery
    {
        public List<Movimentacao> Movimentacoes { get; set; }
        public string ProdutoId { get; set; }
        public int QuantidadeEntrada { get; set; }
        public int QuantidadeSaida { get; set; }
        public int QuantidadeTotal { get; set; }

        public MovimentacaoProdutoQuantidadesQuery(List<Movimentacao> movimentacoes, string produtoId, int quantidadeEntrada, int quantidadeSaida, int quantidadeTotal)
        {
            ProdutoId = produtoId;
            Movimentacoes = movimentacoes;
            QuantidadeEntrada = quantidadeEntrada;
            QuantidadeSaida = quantidadeSaida;
            QuantidadeTotal = quantidadeTotal;
        }
    }
}