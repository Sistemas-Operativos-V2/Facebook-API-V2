using FacebookAPI.Models;
using FacebookAPI.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FacebookAPI.Services
{
    public class PublicationsService
    {
        private readonly IMongoCollection<Publications> _publicationsCollection;

        public PublicationsService(IOptions<FacebookDatabaseSettings> publicationsDatabaseSettings)
        {
            var mongoClient = new MongoClient(publicationsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(publicationsDatabaseSettings.Value.DatabaseName);
            _publicationsCollection = mongoDatabase.GetCollection<Publications>(publicationsDatabaseSettings.Value.PublicationsCollectionName);
        }

        public async Task<List<Publications>> GetAsync() =>
            await _publicationsCollection.Find(_ => true).ToListAsync();

        public async Task<Publications?> GetAsync(string id) =>
            await _publicationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Publications newPublication) =>
            await _publicationsCollection.InsertOneAsync(newPublication);

        public async Task UpdateAsync(string id, Publications updatedPublication) =>
            await _publicationsCollection.ReplaceOneAsync(x => x.Id == id, updatedPublication);

        public async Task RemoveAsync(string id) =>
            await _publicationsCollection.DeleteOneAsync(x => x.Id == id);
    }

}
