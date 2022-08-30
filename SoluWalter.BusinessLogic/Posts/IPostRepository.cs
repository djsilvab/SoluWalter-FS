using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluWalter.BusinessLogic.Posts
{
    public interface IPostRepository
    {
        Task<Post> GetById(string id);
        Task<List<Post>> GetListPosts();
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task DeleteById(string id);

    }
}
