using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sayeed.NTier.Generic.Logic
{
    public interface IBaseService<T> where T : class
    {
        public Task<int> SaveChangesAsync();

        #region basic crud
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> FindByIdAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public Task Update(T item);
        public Task UpdateById(long id, T item);
        public Task Delete(T item);
        public Task DeleteByIdAsync(long id);
        #endregion

    }
}
