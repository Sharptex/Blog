using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roles;
        public RoleService(IRepository<Role> roles)
        {
            _roles = roles;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await _roles.CreateAsync(role);
            return role;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var data = await _roles.GetAllAsync();
            return data;
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var data = await _roles.GetAsync(id);
            return data;
        }

        public async Task<int> UpdateAsync(Role tag)
        {
            var data = await _roles.UpdateAsync(tag);
            return data;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var data = await _roles.DeleteAsync(id);
            return data;
        }
    }
}
