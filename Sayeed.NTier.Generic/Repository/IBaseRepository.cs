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

        /// <summary>
        /// Example usage with filters, includes, and order by
        /// var results = repository.GetAllAsync(
        ///    filter: e => e.SomeProperty == someValue,
        ///    orderBy: q => q.OrderBy(e => e.SomeProperty),
        ///    includes: new List<Expression<Func<YourEntity, object>>>
        ///    {
        ///       e => e.NavigationProperty,
        ///       e => e.AnotherNavigationProperty
        ///    }).ToList();
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                List<Expression<Func<T, object>>> includes = null,
                                                int page = 1, 
                                                int pageSize = 10);
        public Task<T> FindByIdAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public Task UpdateAsync(T item);
        public Task UpdateByIdAsync(long id, T item);
        public Task DeleteAsync(T item);
        public Task DeleteByIdAsync(long id);

        public Task<int> SaveChangesAsync();

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           List<Expression<Func<T, object>>> includes = null);

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                   List<Expression<Func<T, object>>> includes = null);

    }
}
