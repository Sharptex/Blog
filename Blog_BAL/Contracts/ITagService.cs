using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Contracts
{
    public interface ITagService
    {
        Task<Tag> CreateAsync(Tag tag);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task<int> UpdateAsync(Tag tag);
        Task<int> DeleteAsync(Guid id);
    }
}
