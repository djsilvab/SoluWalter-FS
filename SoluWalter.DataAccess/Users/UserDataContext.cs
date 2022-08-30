using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SoluWalter.Entities.Dtos;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoluWalter.DataAccess.Users
{
    public class UserDataContext : IUserDataContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<User> _collection;

        public UserDataContext(IOptions<DBSettings> dbsettings)
        {
            var mongoClient = new MongoClient(dbsettings.Value.ConnectionString);
            if (mongoClient != null)
            {
                _database = mongoClient.GetDatabase(dbsettings.Value.Database);
                _collection = _database.GetCollection<User>("Users");
            }
        }

        public async Task<User> GetById(string id)
        {
            return await _collection.Find(Builders<User>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            var buildFind = Builders<User>.Filter.Eq("Id", user.Id);
            var buildCreate = Builders<User>.Update
                .Set("Nombres", user.Nombres)
                .Set("Apellidos", user.Apellidos)
                .Set("Username", user.Username)
                .Set("Password", user.Password);
            var options = new FindOneAndUpdateOptions<User, User>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            return await _collection.FindOneAndUpdateAsync(buildFind, buildCreate, options);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var arrayFilter = Builders<User>.Filter.Eq("Username", username) &
                 Builders<User>.Filter.Eq("Password", password);

            return await _collection.Find(arrayFilter).FirstOrDefaultAsync();
        }
    }
}
