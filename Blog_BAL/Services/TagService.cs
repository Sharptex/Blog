using Blog_BLL.Contracts;
using Blog_DAL.Contacts;
using Blog_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BLL.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tags;
        public TagService(IRepository<Tag> tags)
        {
            _tags = tags;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await _tags.CreateAsync(tag);
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var data = await _tags.GetAllAsync();
            return data;
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            var data = await _tags.GetAsync(id);
            return data;
        }

        public async Task<int> UpdateAsync(Tag tag)
        {
            var data = await _tags.UpdateAsync(tag);
            return data;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var data = await _tags.DeleteAsync(id);
            return data;
        }
    }
}
