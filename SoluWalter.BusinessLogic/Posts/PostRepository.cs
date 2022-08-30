using SoluWalter.DataAccess.Posts;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluWalter.BusinessLogic.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly IPostDataContext _context;
        public PostRepository(IPostDataContext context)
        {
            this._context = context;
        }

        public async Task<Post> GetById(string id)
        {
            return await _context.GetById(id);
        }

        public async Task<List<Post>> GetListPosts()
        {
            return await _context.GetListPosts();
        }

        public async Task<Post> CreatePost(Post post)
        {
            return await _context.CreatePost(post);
        }

        public async Task<Post> UpdatePost(Post post)
        {            
            return await _context.UpdatePost(post);
        }

        public async Task DeleteById(string id)
        {
            await _context.DeleteById(id);
        }
    }
}
