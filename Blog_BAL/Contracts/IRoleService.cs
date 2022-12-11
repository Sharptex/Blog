using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Contracts
{
    public interface IRoleService
    {
        Task<Role> CreateAsync(Role tag);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetAsync(Guid id);
        Task<int> UpdateAsync(Role tag);
        Task<int> DeleteAsync(Guid id);
    }
}
