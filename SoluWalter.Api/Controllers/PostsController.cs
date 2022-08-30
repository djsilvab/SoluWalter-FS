using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoluWalter.BusinessLogic.Posts;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluWalter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;
        public PostsController(IPostRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get()
        {
            var posts = await _repository.GetListPosts();
            if (posts == null || posts.Count == 0) return NotFound();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(string id)
        {
            var post = await _repository.GetById(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _repository.CreatePost(post);
            if (result == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(string id, Post post)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id.Equals(0) || !id.Equals(post.Id)) return BadRequest();
            var postFind = await _repository.GetById(id);
            if (postFind == null) return NotFound();

            var result = await _repository.UpdatePost(post);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(string id)
        {
            var post = await _repository.GetById(id);
            if (post == null) return NotFound();
            await _repository.DeleteById(id);
            return NoContent();
        }
    }
}
