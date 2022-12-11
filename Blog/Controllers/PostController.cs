using AutoMapper;
using Blog.DTO;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Add(PostDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            Post post = _mapper.Map<Post>(dto);
            post.Created_at = DateTimeOffset.Now;
            post.Updated_at = DateTimeOffset.Now;

            var result = await _postService.CreatePostWithTags(User, post);

            var data = _mapper.Map<PostDTO>(result);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _postService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PostDTO>>(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<PostDTO>(data);
            return Ok(result);
        }

        [Route("GetByAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetByAuthorIdAsync(string id)
        {
            var data = await _accountService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<IEnumerable<PostDTO>>(data.Posts);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Guid id, PostDTO dto)
        {
            Post post = _mapper.Map<Post>(dto);
            post.Id = id;
            post.Updated_at = DateTimeOffset.Now;

            var data = await _postService.UpdateAsync(User, post);
            return Ok(data);
        }

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
