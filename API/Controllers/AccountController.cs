using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_DAL.Models;
using Blog_BLL.Contracts;
using API.DTO;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Attempts to register the specified user.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        public async Task<IActionResult> Register(UserProfileDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            var data = await _accountService.Create(user, dto.Password, dto.Roles);

            if (!data)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Attempts to sign in the specified user and password combination.
        /// </summary>
        /// <param name="dto"></param>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _accountService.Login(dto.Login, dto.Password, dto.RememberMe);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Signs the current user out.
        /// </summary>
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return Ok();
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Collection of all users</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _accountService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<UserDTO>>(data);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific user found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var data = await _accountService.GetAsync(id);
            if (data == null) return NotFound();

            var result = _mapper.Map<UserDTO>(data);
            return Ok(result);
        }

        /// <summary>
        /// Updates a user item.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="currentPasword"></param>
        /// <returns>IdentityResult of operation</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UserDTO dto, string currentPasword)
        {
            var user = await _accountService.GetAsync(dto.Id.ToString());
            if (user == null) return NotFound();

            await _accountService.UpdatePasswordAsync(user, currentPasword, dto.Password);
            await _accountService.AddRolesAndClaimsAsync(user, dto.Roles);

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Login;

            var data = await _accountService.UpdateAsync(user);
            return Ok(data);
        }

        /// <summary>
        /// Deletes a specific user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A number of items affected</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _accountService.GetAsync(id);
            if (user == null) return NotFound();
            var result = await _accountService.DeleteAsync(user);
            return Ok(result);
        }
    }
}
