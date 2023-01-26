using API.DTO;
using AutoMapper;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;

        public PostController(IPostService postService, IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a post item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created post</returns>
        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        public async Task<IActionResult> Add(PostDTO dto)
        {
            Post post = _mapper.Map<Post>(dto);
            post.Created_at = DateTimeOffset.Now;
            post.Updated_at = DateTimeOffset.Now;

            var result = await _postService.CreatePostWithTags(User, post);

            var data = _mapper.Map<PostDTO>(result);
            return Ok(data);
        }

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <returns>Collection of all posts</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _postService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PostDTO>>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific post by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific post found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<PostDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific post by author id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific post found</returns>
        [Route("GetByAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetByAuthorIdAsync(string id)
        {
            var data = await _accountService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<IEnumerable<PostDTO>>(data.Posts);
            return Ok(result);
        }

        /// <summary>
        /// Updates a post item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A number of posts affected</returns>
        [HttpPut]
        public async Task<IActionResult> Update(PostDTO dto)
        {
            Post post = _mapper.Map<Post>(dto);
            post.Updated_at = DateTimeOffset.Now;

            var data = await _postService.UpdateAsync(User, post);
            return Ok(data);
        }

        /// <summary>
        /// Deletes a specific post by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A number of items affected</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = await _postService.DeleteAsync(id);
            return Ok(data);
        }
    }
}
