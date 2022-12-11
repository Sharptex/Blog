using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _comments;
        public CommentService(IRepository<Comment> comments)
        {
            _comments = comments;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _comments.CreateAsync(comment);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            var data = await _comments.GetAllAsync();
            return data;
        }

        public async Task<Comment> GetAsync(Guid id)
        {
            var data = await _comments.GetAsync(id);
            return data;
        }

        public async Task<int> UpdateAsync(Comment comments)
        {
            var data = await _comments.UpdateAsync(comments);
            return data;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var data = await _comments.DeleteAsync(id);
            return data;
        }
    }
}
