using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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

        #endregion


        /// <summary>
        /// Need to be called after certain operations on db: add update delete, otherwise changes will not be saved..
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }



        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int page = 1,
            int pageSize = 10)
        {
            IQueryable<T> query = this.PrepareQuery(filter, orderBy, includes);
            return await query
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = this.PrepareQuery(filter, orderBy, includes);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = this.PrepareQuery(filter, orderBy, includes);
            return await query.SingleOrDefaultAsync();
        }

        private IQueryable<T> PrepareQuery(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            // Default OrderBy Id descending if orderBy is not provided
            if (orderBy == null)
            {
                var primaryKeyProperty = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
                if (primaryKeyProperty != null && primaryKeyProperty.Name == "Id")
                {
                    orderBy = q => q.OrderByDescending(entity => EF.Property<object>(entity, "Id"));
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public virtual async Task<T> FindByIdAsync(long id)
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

        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters)
        {
            throw new NotImplementedException("TODO:- '_dbSet.FromSqlRaw' method could not be resolved.");
            //return _dbSet.FromSqlRaw(rawsql, parameters);
        }

    }
}
