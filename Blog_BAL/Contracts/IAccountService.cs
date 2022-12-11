using Blog_DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Contracts
{
    public interface IAccountService
    {
        Task<bool> Create(User user, string password, ICollection<Guid> roleIds);
        Task SignInAsync(User user, bool rememberMe);
        Task<SignInResult> Login(string login, string password, bool rememberMe);
        Task SignOutAsync();
        Task<bool> AddRolesAndClaimsAsync(User user, ICollection<Guid> roleIds);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string id);
        Task<IdentityResult> UpdateAsync(User user);
        Task<int> AddRoleAsync(User user, Role role);
        Task<IdentityResult> DeleteAsync(User user);
        Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword);
    }
}
