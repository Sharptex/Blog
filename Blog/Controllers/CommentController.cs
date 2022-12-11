using AutoMapper;
using Blog.DTO;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public async Task<IActionResult> Add(CommentDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            if (_accountService.GetAsync(dto.Author_id).Result == null || _postService.GetAsync(dto.Post_id).Result == null)
            {
                return BadRequest("Invalid id for post or author entity");
            }


            Comment comment = _mapper.Map<Comment>(dto);
            comment.Created_at = DateTimeOffset.Now;
            comment.Updated_at = DateTimeOffset.Now;

            var data = await _commentService.CreateAsync(comment);
            var result = _mapper.Map<CommentDTO>(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _commentService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CommentDTO>>(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<CommentDTO>(data);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CommentDTO dto)
        {
            Comment comment = _mapper.Map<Comment>(dto);
            comment.Id = id;
            comment.Updated_at = DateTimeOffset.Now;

            var result = await _commentService.UpdateAsync(comment);
            return Ok(result);
        }

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
