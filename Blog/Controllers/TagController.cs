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
    public class TagController : Controller
    {
        private readonly ITagService tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            this.tagService = tagService;
            _mapper = mapper;
        }

        [Route("TagAdd")]
        [HttpGet]
        public IActionResult Index()
        {
            var response = new TagViewModel();
            return View("Index", response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            Tag tag = _mapper.Map<Tag>(dto);
            var data = await tagService.CreateAsync(tag);
            //var result = _mapper.Map<TagDTO>(data);
            //return Ok(result);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await tagService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TagViewModel>>(data);
            //return Ok(result);
            return View("TagList", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await tagService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<TagViewModel>(data);
            //return Ok(result);
            return View("TagEditor", result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TagDTO dto)
        {
            Tag tag = _mapper.Map<Tag>(dto);
            //tag.Id = id;
            var result = await tagService.UpdateAsync(tag);
            //return Ok(result);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await tagService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await tagService.DeleteAsync(id);
            //return Ok(result);
            return RedirectToAction("GetAll");
        }
    }
}
