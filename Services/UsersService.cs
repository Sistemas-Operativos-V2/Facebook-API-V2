using FacebookAPI.Models;
using FacebookAPI.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FaceUserAPI.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<Users> _usersCollection;

        public UsersService(
            IOptions<FacebookDatabaseSettings> UserStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                UserStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                UserStoreDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Users>(
                UserStoreDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<Users>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Users?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Users newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, Users updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
