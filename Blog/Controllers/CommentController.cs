using AutoMapper;
using Blog.DTO;
using Blog.ViewModels;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CommentController : Controller
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

        [Route("CommentCreate")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await _postService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PostViewModel>>(data);

            return View("CommentCreate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAndCommentsViewModel dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            Comment comment = _mapper.Map<Comment>(dto.Comment);
            comment.Created_at = DateTimeOffset.Now;
            comment.Updated_at = DateTimeOffset.Now;
            comment.Post_id = dto.Id;

            var data = await _commentService.CreateAsync(User, comment);

            return RedirectToAction("Index", "Post", new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _commentService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CommentViewModel>>(data);
            //return Ok(result);
            return View("CommentList", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<CommentViewModel>(data);
            //return Ok(result);
            return View("CommentEditor", result);
        }

        [Route("Comment/Index")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<CommentViewModel>(data);
            //return Ok(result);
            return View("Index", result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CommentViewModel dto, Guid postId, string authorId)
        {
            Comment comment = _mapper.Map<Comment>(dto);
            comment.Updated_at = DateTimeOffset.Now;
            //var user = await _accountService.GetAsync(authorId);
            //var post = await _postService.GetAsync(postId);
            comment.Author_id = authorId;
            comment.Post_id = postId;

            var result = await _commentService.UpdateAsync(comment);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _commentService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await _commentService.DeleteAsync(id);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }
    }
}
