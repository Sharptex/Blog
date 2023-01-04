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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [Route("RoleCreate")]
        [HttpGet]
        public IActionResult Create()
        {
            var response = new RoleViewModel();
            return View("RoleCreate", response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            Role role = _mapper.Map<Role>(dto);
            
            var data = await _roleService.CreateAsync(role);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _roleService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RoleViewModel>>(data);

            return View("RoleList", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<RoleViewModel>(data);

            return View("RoleEditor", result);
        }

        [Route("Role/Index")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<RoleViewModel>(data);

            return View("Index", result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel dto)
        {
            Role role = _mapper.Map<Role>(dto);
            var result = await _roleService.UpdateAsync(role);

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _roleService.GetAsync(id);
            if (data == null) return NotFound();

            var result = await _roleService.DeleteAsync(id);

            return RedirectToAction("GetAll");
        }
    }
}
