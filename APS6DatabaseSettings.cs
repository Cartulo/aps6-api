namespace Aps6Api;

public class APS6DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ProdutosCollectionName { get; set; } = null!;
    public string MovimentacoesCollectionName { get; set; } = null!;
    public string SetoresCollectionName { get; set; } = null!;

}