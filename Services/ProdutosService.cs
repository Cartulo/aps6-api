using Aps6Api;
using Aps6Api.Produtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Aps6Api.Services
{
    public class ProdutosService
    {
        private readonly IMongoCollection<Produto> _produtosCollection;

    public ProdutosService(
        IOptions<APS6DatabaseSettings> aps6DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            aps6DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            aps6DatabaseSettings.Value.DatabaseName);

        _produtosCollection = mongoDatabase.GetCollection<Produto>(
            aps6DatabaseSettings.Value.ProdutosCollectionName);
    }

    public async Task<List<Produto>> GetAsync() =>
        await _produtosCollection.Find(_ => true).ToListAsync();

    public async Task<Produto?> GetAsync(string id) =>
        await _produtosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Produto novoProduto) =>
        await _produtosCollection.InsertOneAsync(novoProduto);

    public async Task UpdateAsync(string id, Produto editarProduto) =>
        await _produtosCollection.ReplaceOneAsync(x => x.Id == id, editarProduto);

    public async Task RemoveAsync(string id) =>
        await _produtosCollection.DeleteOneAsync(x => x.Id == id);
    }
}