using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Sayeed.NTier.Generic.Repository
{
    // TODO: I want to make it abstract, but failing to do it!
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        #region initializations & declarations

        private readonly DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #endregion initializations & declarations

        /// <summary>
        /// Need to be called after certain operations on db: add update delete, otherwise changes will not be saved..
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #region basic crud operations

        public virtual async Task<IEnumerable<T>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _dbSet
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public virtual async Task AddAsync(T item)
        {
            item.GetType().GetProperty("Id")?.SetValue(item, 0); // setting the PK of the row as 0 when the PK is Id int
            await _dbSet.AddAsync(item);
            // SaveChangesAsync will be called from UnitOfWork!
        }

        public virtual void UpdateById(long id, T item)
        {
            // trick from StackOverFlow
            var t = item.GetType();
            var prop = t.GetProperty("Id");
            var itemId = (long)prop.GetValue(item);

            if (id != itemId) throw new Exception("Access restricted!");

            Update(item);
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
            // SaveChangesAsync will be called from UnitOfWork!
        }

        public virtual void Delete(T item)
        {
            _dbSet.Remove(item);
            // SaveChangesAsync will be called from UnitOfWork!
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            var itemToBeDeleted = await _dbSet.FindAsync(id);

            if (itemToBeDeleted == null) throw new Exception("Item not found!");

            Delete(itemToBeDeleted);
        }

        #endregion basic crud operations

        #region advanced crud operations

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var queryable = _dbSet.AsQueryable();
            // foreach (Expression<Func<T, object>> i in includes) // another way of iterating over..
            for (var i = 0; i < includes.Length; i++) queryable.Include(includes[i]);
            var ret = await queryable.SingleOrDefaultAsync(filter);
            return ret ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// This Where() doesn't support ThenInclude().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var ret = _dbSet.Where(filter);
            foreach (var i in includes)
                ret = ret.Include(i);
            return ret;
        }

        public async Task<long> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        /// <summary>
        /// This FirstOrDefaultAsync() doesn't support ThenInclude().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            T? ret;
            if (includes.Length > 0)
            {
                IQueryable<T> queryable = _dbSet.Include(includes[0]);
                for (var i = 1; i < includes.Length; i++)
                {
                    queryable.Include(includes[i]);
                }
                ret = await queryable.FirstOrDefaultAsync(filter);
            }
            else
            {
                ret = await _dbSet.FirstOrDefaultAsync(filter);
            }
            return ret ?? throw new InvalidOperationException();
        }

        // Since LastOrDefault doesn't support anymore, we customized it!
        public async Task<T> LastOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            var queryable = _dbSet.AsQueryable();
            for (var i = 0; i < includes.Length; i++) queryable.Include(includes[i]);

            var ret = await queryable.Skip(_dbSet.Count() - 1).FirstOrDefaultAsync();

            return ret ?? throw new InvalidOperationException();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> GetByWhereClause(
            Expression<Func<T, bool>> filter,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        )
        {
            var queryable = _dbSet.Where(filter);

            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return queryable;
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> GetBySingleOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        )
        {
            var queryable = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.SingleOrDefaultAsync(filter) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> GetByFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        )
        {
            var queryable = _dbSet.AsQueryable();

            //use aggregate
            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.FirstOrDefaultAsync(filter) ?? throw new InvalidOperationException();
        }

        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters)
        {
            throw new NotImplementedException("TODO:- '_dbSet.FromSqlRaw' method could not be resolved.");
            //return _dbSet.FromSqlRaw(rawsql, parameters);
        }

        #endregion advanced crud operations
    }
}