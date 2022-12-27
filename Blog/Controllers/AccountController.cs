using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_DAL.Models;
using Blog.DTO;
using Blog_BLL.Contracts;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountController : Controller
    {
        private IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public AccountController(IAccountService accountService, IRoleService roleService, IMapper mapper)
        {
            _accountService = accountService;
            _roleService = roleService;
            _mapper = mapper;
        }


        [Route("UserGet")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = new UserViewModel();
            var allRoles = await _roleService.GetAllAsync();
            var allRolesVM = _mapper.Map<IEnumerable<RoleViewModel>>(allRoles);
            response.Roles = allRolesVM.ToList();
            return View("Index", response);
        }

        [Route("RegisterGet")]
        [HttpGet]
        public IActionResult RegisterGet()
        {
            var response = new RegisterViewModel();
            return View("Register", response);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            User user = _mapper.Map<User>(dto);

            var data = await _accountService.Create(user, dto.PasswordReg, dto.Roles);
            //await _accountService.SignInAsync(user, true);

            if (!data)
            {
                ModelState.AddModelError(string.Empty, "Bad registration");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //return Ok();
            return RedirectToAction("LoginGet");
        }

        [Route("")]
        [Route("LoginGet")]
        //[Route("[controller]/[action]")]
        [HttpGet]
        public IActionResult LoginGet()
        {
            var response = new LoginViewModel();
            return View("Login", response);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel dto)
        {
            if (dto == null)
            {
                return BadRequest("DTO object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var result = await _accountService.Login(dto.Login, dto.Password, dto.RememberMe);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", "Неправильный логин и (или) пароль");
                return BadRequest(ModelState);
            }

            //return Ok();
            return RedirectToAction("LoginGet");
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            //return Ok();
            return RedirectToAction("LoginGet");
        }

        //[Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _accountService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<UserViewModel>>(data);
            //return Ok(result);
            return View("UserList", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _accountService.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<UserViewModel>(data);

            var allRoles = await _roleService.GetAllAsync();
            var allRolesVM = _mapper.Map<IEnumerable<RoleViewModel>>(allRoles);
            var vm = allRolesVM.Select(x => { if (result.Roles.Any(y => y.Id == x.Id)) { x.Selected = true; return x; } else { return x; } }).ToList();
            result.Roles = vm;

            //return Ok(result);
            return View("UserEditor", result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel dto, string currentPasword)
        {
            var user = await _accountService.GetAsync(dto.Id.ToString());
            if (user == null) return NotFound();

            await _accountService.UpdatePasswordAsync(user, dto.CurrentPassword, dto.Password);
            await _accountService.AddRolesAndClaimsAsync(user, dto.Roles.Select(v => v.Id).ToList());

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Login;

            var data = await _accountService.UpdateAsync(user);
            //return Ok(data);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _accountService.GetAsync(id);
            if (user == null) return NotFound();
            var result = await _accountService.DeleteAsync(user);
            //return Ok(result);
            return RedirectToAction("GetAll");
        }
    }
}
