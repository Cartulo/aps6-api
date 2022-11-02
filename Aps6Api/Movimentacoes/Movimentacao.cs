namespace Aps6Api.Movimentacoes
{
    public class Movimentacao
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public int QuantidadeMinima { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }        
    }
}