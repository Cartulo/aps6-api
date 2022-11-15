namespace Aps6Api.Movimentacoes.Requests
{
    public class ObterQuantidadesPorSetorRequest
    {
        public string SetorId { get; set; }
        public string ProdutoId { get; set; }

        public ObterQuantidadesPorSetorRequest(string setorId, string produtoId)
        {
            SetorId = setorId;
            ProdutoId = produtoId;
        }
    }
}