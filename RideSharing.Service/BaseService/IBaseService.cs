using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public interface IBaseService<T> where T : class
    {
        public Task<int> SaveChangesAsync();

        #region basic crud
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> FindAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public void Update(T item);
        public void UpdateById(long id, T item);
        public void Delete(T item);
        public Task DeleteByIdAsync(long id);
        #endregion

    }
}
