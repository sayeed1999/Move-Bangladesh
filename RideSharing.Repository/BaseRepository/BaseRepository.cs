using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RideSharing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Repository
{
    // TODO: I want to make it abstract, but failing to do it!
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        #region initializations & declarations

        private readonly ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #endregion


        /// <summary>
        /// Need to be called after certain operations on db: add update delete, otherwise changes will not be saved..
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }



        #region basic crud operations

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> FindAsync(long id)
        {
            return await _dbSet.FindAsync(id);
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
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty("Id");
            long itemId = (long)prop.GetValue(item);

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

        #endregion



        #region advanced crud operations

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            T? ret = null;
            IQueryable<T> queryable = _dbSet.AsQueryable();
            // foreach (Expression<Func<T, object>> i in includes) // another way of iterating over..
            for (int i = 0; i < includes.Length; i++) queryable.Include(includes[i]);
            ret = await queryable.SingleOrDefaultAsync(filter);
            return ret;
        }

        /// <summary>
        /// This Where() doesn't support ThenInclude().
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> ret = _dbSet.Where(filter);
            foreach (Expression<Func<T, object>> i in includes)
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
            T? ret = null;
            if (includes.Length > 0)
            {
                IQueryable<T> queryable = _dbSet.Include(includes[0]);
                for (int i = 1; i < includes.Length; i++)
                {
                    queryable.Include(includes[i]);
                }
                ret = await queryable.FirstOrDefaultAsync(filter);
            }
            else
            {
                ret = await _dbSet.FirstOrDefaultAsync(filter);
            }
            return ret;
        }

        // Since LastOrDefault doesn't support anymore, we customized it!
        public async Task<T> LastOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            T? ret = null;
            IQueryable<T> queryable = _dbSet.AsQueryable();
            for (int i = 0; i < includes.Length; i++) queryable.Include(includes[i]);

            ret = await queryable.Skip(_dbSet.Count() - 1).FirstOrDefaultAsync();

            return ret;
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
            IQueryable<T> queryable = _dbSet.Where(filter);

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
            IQueryable<T> queryable = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.SingleOrDefaultAsync(filter);
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
            IQueryable<T> queryable = _dbSet.AsQueryable();

            //use aggregate
            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.FirstOrDefaultAsync(filter);
        }

        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters)
        {
            return _dbSet.FromSqlRaw(rawsql, parameters);
        }

        #endregion

    }
}
