using AutoMapper;
using Blog.DTO;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            this.tagService = tagService;
            _mapper = mapper;
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
            var result = _mapper.Map<TagDTO>(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await tagService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TagDTO>>(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await tagService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<TagDTO>(data);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, TagDTO dto)
        {
            Tag tag = _mapper.Map<Tag>(dto);
            tag.Id = id;
            var result = await tagService.UpdateAsync(tag);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await tagService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await tagService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
