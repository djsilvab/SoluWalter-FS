using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluWalter.DataAccess.Posts
{
    public class PostDataContext : IPostDataContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Post> _collection;
        public PostDataContext(IOptions<DBSettings> dbsettings)
        {
            var mongoClient = new MongoClient(dbsettings.Value.ConnectionString);
            if (mongoClient != null)
            {
                _database = mongoClient.GetDatabase(dbsettings.Value.Database);
                _collection = _database.GetCollection<Post>("Posts");
            }
        }
        public async Task<Post> GetById(string id)
        {
            //var _id = new ObjectId(id);
            var buildFind = Builders<Post>.Filter.Eq("Id", id);
            return await _collection.Find(buildFind).FirstOrDefaultAsync();                
        }
        public async Task<List<Post>> GetListPosts()
        {
            var posts = await _collection.Find(d => true).ToListAsync();            
            return posts;
            
        }
        public async Task<Post> CreatePost(Post post)
        {
            post.Id = ObjectId.GenerateNewId().ToString();
            var buildFind = Builders<Post>.Filter.Eq("Id", post.Id);
            var buildCreate = Builders<Post>.Update
                .Set("Titulo", post.Titulo)
                .Set("Descripcion", post.Descripcion)
                .Set("Autor", post.Autor)
                .Set("Categoria", post.Categoria);
            var options = new FindOneAndUpdateOptions<Post, Post>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            return await _collection.FindOneAndUpdateAsync(buildFind, buildCreate, options);

            //post.Id = ObjectId.GenerateNewId().ToString();
            //await _collection.InsertOneAsync(post);
            //return await GetById(post.Id);
        }
        public async Task<Post> UpdatePost(Post post)
        {
            //return await Task.FromResult(new Post());            
            var buildFind = Builders<Post>.Filter.Eq("Id", post.Id);
            var buildUpdate = Builders<Post>.Update
                .Set("Titulo", post.Titulo)
                .Set("Descripcion", post.Descripcion)
                .Set("Autor", post.Autor)
                .Set("Categoria", post.Categoria);
            var options = new FindOneAndUpdateOptions<Post, Post>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            return await _collection.FindOneAndUpdateAsync(buildFind, buildUpdate, options);
        }
        public async Task DeleteById(string id)
        {
            var buildFind = Builders<Post>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(buildFind);
        }
    }
}
