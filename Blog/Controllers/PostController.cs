using AutoMapper;
using Blog.DTO;
using Blog.ViewModels;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PostController : Controller
    {
        private IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;

        public PostController(IPostService postService, IAccountService accountService, ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _accountService = accountService;
            _postService = postService;
            _mapper = mapper;
        }

        [Route("PostCreate")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = new PostViewModel();
            var allTags = await _tagService.GetAllAsync();
            var allTagsVM = _mapper.Map<IEnumerable<TagViewModel>>(allTags);
            response.Tags = allTagsVM.ToList();
            return View("PostCreate", response);
        }

        //[Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel dto)
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

            //var data = _mapper.Map<PostDTO>(result);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }

        //[Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _postService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PostViewModel>>(data);
            //return Ok(result);
            return View("PostList", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<PostViewModel>(data);

            var allTags = await _tagService.GetAllAsync();
            var allTagsVM = _mapper.Map<IEnumerable<TagViewModel>>(allTags);
            var vm = allTagsVM.Select(x => { if (result.Tags.Any(y => y.Id == x.Id)) { x.Selected = true; return x; } else { return x; } }).ToList();
            result.Tags = vm;

            //return Ok(result);
            return View("PostEditor", result);
        }

        [Route("Post/Index")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<PostAndCommentsViewModel>(data);

            result.Comments = result.Comments.OrderBy(x => x.Created_at).ToList();

            result.Comment = new CommentViewModel();
            //return Ok(result);
            return View("Index", result);
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


        [HttpPost]
        public async Task<IActionResult> Update(PostViewModel dto)
        {
            Post post = _mapper.Map<Post>(dto);
            post.Updated_at = DateTimeOffset.Now;
            post.Tags.Clear();
            var updateCounter2 = await _postService.UpdateAsync(User, post);

            var oldPost = await _postService.GetAsync(dto.Id);
            
            foreach (var item in dto.Tags)
            {
                var tagEntity = await _tagService.GetAsync(item.Id);
                var tagPost = oldPost.Tags.FirstOrDefault(c => c.Id == item.Id);

                if (item.Selected && tagPost == null)
                {
                    oldPost.Tags.Add(tagEntity);
                }
                else if (!item.Selected && tagPost != null)
                {
                    oldPost.Tags.Remove(tagEntity);
                }
            }

            var updateCounter1 = await _postService.UpdateAsync(User, oldPost);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _postService.GetAsync(id);
            if (data == null) return NotFound();
            var result = await _postService.DeleteAsync(id);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }
    }
}
