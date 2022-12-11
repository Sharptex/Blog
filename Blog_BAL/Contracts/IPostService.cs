using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Contracts
{
    public interface IPostService
    {
        Task<Post> CreatePostWithTags(ClaimsPrincipal cp, Post post);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetAsync(Guid id);
        Task<int> UpdateAsync(ClaimsPrincipal cp, Post post);
        Task<int> DeleteAsync(Guid id);
    }
}
