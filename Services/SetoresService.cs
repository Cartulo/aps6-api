using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aps6Api.Setores;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Aps6Api.Services
{
    public class SetoresService
    {
        private readonly IMongoCollection<Setor> _setoresCollection;

    public SetoresService(
        IOptions<APS6DatabaseSettings> aps6DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            aps6DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            aps6DatabaseSettings.Value.DatabaseName);

        _setoresCollection = mongoDatabase.GetCollection<Setor>(
            aps6DatabaseSettings.Value.SetoresCollectionName);
    }

    public async Task<List<Setor>> GetAsync() =>
        await _setoresCollection.Find(_ => true).ToListAsync();

    public async Task<Setor?> GetAsync(string id) =>
        await _setoresCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Setor novoSetor) =>
        await _setoresCollection.InsertOneAsync(novoSetor);

    public async Task UpdateAsync(string id, Setor editarSetor) =>
        await _setoresCollection.ReplaceOneAsync(x => x.Id == id, editarSetor);

    public async Task RemoveAsync(string id) =>
        await _setoresCollection.DeleteOneAsync(x => x.Id == id);
    }
}