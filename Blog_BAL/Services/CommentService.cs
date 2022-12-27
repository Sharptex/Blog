using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Comment> _comments;
        public CommentService(IRepository<Comment> comments, UserManager<User> userManager)
        {
            _comments = comments;
            _userManager = userManager;
        }

        public async Task<Comment> CreateAsync(ClaimsPrincipal cp, Comment comment)
        {
            User user = await _userManager.GetUserAsync(cp);
            comment.Author = user;
            comment.Author_id = user.Id;

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
