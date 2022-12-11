using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Contracts
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(Comment tag);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetAsync(Guid id);
        Task<int> UpdateAsync(Comment tag);
        Task<int> DeleteAsync(Guid id);
    }
}
