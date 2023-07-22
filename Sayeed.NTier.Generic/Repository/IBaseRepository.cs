using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sayeed.NTier.Generic.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public DbSet<T> DbSet { get; }

        #region basic crud
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                List<Expression<Func<T, object>>> includes = null,
                                                int page = 1, 
                                                int pageSize = 10);
        public Task<T> FindByIdAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public void Update(T item);
        public void UpdateById(long id, T item);
        public void Delete(T item);
        public Task DeleteByIdAsync(long id);
        #endregion

        public Task<int> SaveChangesAsync();

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           List<Expression<Func<T, object>>> includes = null);

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                   List<Expression<Func<T, object>>> includes = null);

    }
}
