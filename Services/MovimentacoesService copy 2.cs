using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aps6Api.Movimentacoes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Aps6Api.Services
{
    public class MovimentacoesService
    {
        private readonly IMongoCollection<Movimentacao> _movimentacoesCollection;

    public MovimentacoesService(
        IOptions<APS6DatabaseSettings> aps6DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            aps6DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            aps6DatabaseSettings.Value.DatabaseName);

        _movimentacoesCollection = mongoDatabase.GetCollection<Movimentacao>(
            aps6DatabaseSettings.Value.MovimentacoesCollectionName);
    }

    public async Task<List<Movimentacao>> GetAsync() =>
        await _movimentacoesCollection.Find(_ => true).ToListAsync();

    public async Task<Movimentacao?> GetAsync(string id) =>
        await _movimentacoesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Movimentacao novoMovimentacao) =>
        await _movimentacoesCollection.InsertOneAsync(novoMovimentacao);

    public async Task UpdateAsync(string id, Movimentacao editarMovimentacao) =>
        await _movimentacoesCollection.ReplaceOneAsync(x => x.Id == id, editarMovimentacao);

    public async Task RemoveAsync(string id) =>
        await _movimentacoesCollection.DeleteOneAsync(x => x.Id == id);
    }
}