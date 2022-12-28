using Blog_DAL.Contacts;
using Blog_DAL.Data;
using Blog_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _db;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T> CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }

            return await _db.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            if (typeof(T) == typeof(Post)) 
            {
                return (await (_dbSet as DbSet<Post>).Include(p => p.Tags).Include(p => p.Comments).ThenInclude(comm => comm.Author).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id)) as T;
            }

            if (typeof(T) == typeof(Tag))
            {
                return (await (_dbSet as DbSet<Tag>).Include(p => p.Posts).FirstOrDefaultAsync(p => p.Id == id)) as T;
            }

            if (typeof(T) == typeof(Role))
            {
                return (await (_dbSet as DbSet<Role>).Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == id)) as T;
            }

            if (typeof(T) == typeof(Comment))
            {
                return (await (_dbSet as DbSet<Comment>).Include(p => p.Author).Include(p => p.Post).FirstOrDefaultAsync(p => p.Id == id)) as T;
            }

            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Post))
            {
                return (await (_dbSet as DbSet<Post>).Include(p => p.Tags).ToListAsync()) as List<T>;
            }
            if (typeof(T) == typeof(Role))
            {
                return (await (_dbSet as DbSet<Role>).Include(p => p.Users).ToListAsync()) as List<T>;
            }
            if (typeof(T) == typeof(Tag))
            {
                return (await (_dbSet as DbSet<Tag>).Include(p => p.Posts).ToListAsync()) as List<T>; 
            }
            if (typeof(T) == typeof(Comment))
            {
                return (await (_dbSet as DbSet<Comment>).Include(p => p.Author).Include(p=>p.Post).ToListAsync()) as List<T>;
            }

            return await _dbSet.ToListAsync();
        }

        public async Task<int> UpdateAsync(T item)
        {
            _db.Update(item);

            return await _db.SaveChangesAsync();
        }
    }
}
