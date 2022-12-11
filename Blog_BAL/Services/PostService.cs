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
            await CreateAndUpdateTags(post, post.Tags);
            await _posts.CreateAsync(post);
            return post;
        }

        private async Task CreateAndUpdateTags(Post post, ICollection<Tag> tags)
        {
            var tagList = await _tags.GetAllAsync();
            var newTagList = new List<Tag>();

            foreach (var item in tags)
            {
                var tagItem = tagList.FirstOrDefault(x => x.Name == item.Name);

                if (tagItem == null)
                {
                    tagItem = await _tags.CreateAsync(item);
                }

                newTagList.Add(tagItem);
            }

            post.Tags = newTagList;
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
            await CreateAndUpdateTags(post, post.Tags);
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
