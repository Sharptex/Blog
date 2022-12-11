using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_DAL.Contacts
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T item);
        Task<T> GetAsync(Guid id);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
