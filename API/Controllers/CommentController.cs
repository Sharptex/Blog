using API.DTO;
using AutoMapper;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IPostService postService, IAccountService accountService, IMapper mapper)
        {
            _commentService = commentService;
            _accountService = accountService;
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a comment item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created comment</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CommentDTO dto)
        {
            if (_accountService.GetAsync(dto.Author_id).Result == null || _postService.GetAsync(dto.Post_id).Result == null)
            {
                return BadRequest("Invalid post or author id");
            }

            Comment comment = _mapper.Map<Comment>(dto);
            comment.Created_at = DateTimeOffset.Now;
            comment.Updated_at = DateTimeOffset.Now;

            var data = await _commentService.CreateAsync(User, comment);
            var result = _mapper.Map<CommentDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get all comments.
        /// </summary>
        /// <returns>Collection of all roles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _commentService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CommentDTO>>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific comment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific comment found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<CommentDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Updates a comment item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A number of items affected</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CommentDTO dto)
        {
            Comment comment = _mapper.Map<Comment>(dto);
            comment.Updated_at = DateTimeOffset.Now;

            var result = await _commentService.UpdateAsync(comment);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific comment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A number of items affected</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await _commentService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
