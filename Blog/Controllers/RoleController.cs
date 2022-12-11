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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            dto.Id = null;
            Role role = _mapper.Map<Role>(dto);
            
            var data = await _roleService.CreateAsync(role);
            var result = _mapper.Map<RoleDTO>(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _roleService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RoleDTO>>(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<RoleDTO>(data);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, RoleDTO dto)
        {
            Role role = _mapper.Map<Role>(dto);
            role.Id = id;
            var result = await _roleService.UpdateAsync(role);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await _roleService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
