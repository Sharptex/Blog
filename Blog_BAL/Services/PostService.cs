using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Blog_BLL.Services
{
    public class PostService : IPostService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Post> _posts;
        private readonly IRepository<Tag> _tags;
        public PostService(IRepository<Tag> tags, IRepository<Post> posts, UserManager<User> userManager)
        {
            _posts = posts;
            _tags = tags;
            _userManager = userManager;
             
        }

        public async Task<Post> CreatePostWithTags(ClaimsPrincipal cp,  Post post)
        {
            User user = await _userManager.GetUserAsync(cp);
            post.Author = user;
            post.Author_id = user.Id;
            var tags = await CreateTagList(post.Tags);
            if (tags == null) { return null; }
            post.Tags = tags;

            var newPost = await _posts.CreateAsync(post);
            return newPost;
        }

        private async Task<List<Tag>> CreateTagList(ICollection<Tag> tags)
        {
            var newTagList = new List<Tag>();

            foreach (var tag in tags)
            {
                var tagItem = await _tags.GetAsync(tag.Id);

                if (tagItem == null)
                {
                    return null;
                }

                newTagList.Add(tagItem);
            }

            return newTagList;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var data = await _posts.GetAllAsync();
            return data;
        }

        public async Task<Post> GetAsync(Guid id)
        {
            var data = await _posts.GetAsync(id);
            return data;
        }

        public async Task<int> UpdateAsync(ClaimsPrincipal cp, Post post)
        {
            User user = await _userManager.GetUserAsync(cp);
            post.Author = user;
            post.Author_id = user.Id;

            var data = await _posts.UpdateAsync(post);
            return data;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var data = await _posts.DeleteAsync(id);
            return data;
        }
    }
}
