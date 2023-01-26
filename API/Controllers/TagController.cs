using API.DTO;
using AutoMapper;
using Blog_BLL.Contracts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            this.tagService = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a tag item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created tag</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(TagDTO dto)
        {
            Tag tag = _mapper.Map<Tag>(dto);
            var data = await tagService.CreateAsync(tag);
            var result = _mapper.Map<TagDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns>Collection of all tags</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await tagService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TagDTO>>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific tag by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific tag found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await tagService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<TagDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Updates a tag item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A number of items affected</returns>
        [HttpPut]
        public async Task<IActionResult> Update(TagDTO dto)
        {
            Tag tag = _mapper.Map<Tag>(dto);
            var result = await tagService.UpdateAsync(tag);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific tag item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A number of items affected</returns>
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
