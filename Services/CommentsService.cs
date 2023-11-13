using FacebookAPI.Models;
using FacebookAPI.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FacebookAPI.Services
{
    public class CommentsService
    {
        private readonly IMongoCollection<Comments> _commentsCollection;

        public CommentsService(IOptions<FacebookDatabaseSettings> commentsDatabaseSettings)
        {
            var mongoClient = new MongoClient(commentsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(commentsDatabaseSettings.Value.DatabaseName);
            _commentsCollection = mongoDatabase.GetCollection<Comments>(commentsDatabaseSettings.Value.CommentsCollectionName);
        }

        public async Task<List<Comments>> GetAsync() =>
            await _commentsCollection.Find(_ => true).ToListAsync();

        public async Task<Comments?> GetAsync(string id) =>
            await _commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Comments newComment) =>
            await _commentsCollection.InsertOneAsync(newComment);

        public async Task UpdateAsync(string id, Comments updatedComment) =>
            await _commentsCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);

        public async Task RemoveAsync(string id) =>
            await _commentsCollection.DeleteOneAsync(x => x.Id == id);
    }

}
