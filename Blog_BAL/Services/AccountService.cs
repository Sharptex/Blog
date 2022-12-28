using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Blog_BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<Role> _roles;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IRepository<Role> roles)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roles = roles;
        }

        public async Task<bool> Create(User user, string password, ICollection<Guid> roleIds)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await AddRolesAndClaimsAsync(user, roleIds)) { return false; }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddRolesAndClaimsAsync(User user, ICollection<Guid> newRoles)
        {
            user.Roles.Clear();
            await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
            await _userManager.UpdateAsync(user);
            var defaultUserRole = _roles.GetAllAsync().Result.FirstOrDefault(x =>x.Name == "DefaultUser");
            if (defaultUserRole == null || !await AddRoleAndClaimAsync(user, defaultUserRole)) { return false; }
            if (newRoles == null) { return true; }

            foreach (var id in newRoles)
            {
                var role = await _roles.GetAsync(id);
                if (role == null)
                {
                    return false;
                }
                if (id != defaultUserRole.Id)
                {
                    if (!await AddRoleAndClaimAsync(user, role)) { return false; }
                }
            }

            return true;
        }

        private async Task<bool> AddRoleAndClaimAsync(User user, Role role)
        {
            if (await AddRoleAsync(user, role) == 0) { return false; };
            var res = await AddClaimAsync(user, role);
            if (!res.Succeeded) { return false; }
            return true;
        }

        private async Task<IdentityResult> AddClaimAsync(User user, Role role)
        {
            var claim = new Claim("Role", role.Name);
            return await _userManager.AddClaimAsync(user, claim);
        }

        public Task SignInAsync(User user, bool rememberMe)
        {
            return _signInManager.SignInAsync(user, rememberMe);
            
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> Login(string login, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null) { return SignInResult.Failed; }

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);

            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var data = await _userManager.Users.Include(p => p.Roles).ToListAsync();
            return data;
        }

        public async Task<User> GetAsync(string id)
        {
            var data = await _userManager.Users.Include(p => p.Roles).Include(n=>n.Posts).Include(n => n.Comments).FirstOrDefaultAsync(x=>x.Id==id);

            return data;
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        public async Task<int> AddRoleAsync(User user, Role role)
        {
            role.Users.Add(user);
            return await _roles.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result;
        }
    }
}
