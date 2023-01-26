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

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a role item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created role</returns>
        [HttpPost]
        public async Task<IActionResult> Add(RoleDTO dto)
        {
            Role role = _mapper.Map<Role>(dto);
            var data = await _roleService.CreateAsync(role);
            var result = _mapper.Map<RoleDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>Collection of all roles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _roleService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RoleDTO>>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific role by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific role found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<RoleDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Updates a role item.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A number of items affected</returns>
        [HttpPut]
        public async Task<IActionResult> Update(RoleDTO dto)
        {
            Role role = _mapper.Map<Role>(dto);
            var result = await _roleService.UpdateAsync(role);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific role by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A number of items affected</returns>
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
