using Microsoft.EntityFrameworkCore;
using RideSharing.Application.Abstractions;
using System.Linq.Expressions;
using System.Reflection;

namespace RideSharing.Infrastructure.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T>
		where T : class
	{

		#region initializations & declarations

		private readonly DbContext _dbContext;
		private DbSet<T> _dbSet;

		public BaseRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<T>();
		}

		#endregion

		public DbSet<T> DbSet
		{
			get { return _dbSet; }
		}

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
			IQueryable<T> query = PrepareQuery(filter, orderBy, includes);
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
			IQueryable<T> query = PrepareQuery(filter, orderBy, includes);
			return await query.FirstOrDefaultAsync();
		}

		public virtual async Task<T> SingleOrDefaultAsync(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			List<Expression<Func<T, object>>> includes = null)
		{
			IQueryable<T> query = PrepareQuery(filter, orderBy, includes);
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

		public virtual async Task<T> FindByIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual async Task<T> AddAsync(T item)
		{
			//item.GetType().GetProperty("Id")?.SetValue(item, 0); // setting the PK of the row as 0 when the PK is Id int
			await _dbSet.AddAsync(item);
			await _dbContext.SaveChangesAsync();
			return item;
		}

		public virtual async Task<T> UpdateByIdAsync(long id, T item)
		{
			Type t = item.GetType();
			PropertyInfo prop = t.GetProperty("Id");
			long itemId = (long)prop.GetValue(item);
			//long itemId = item.Id;

			if (id != itemId) throw new Exception("Access restricted!");

			return await UpdateAsync(item);
		}

		public virtual async Task<T> UpdateAsync(T item)
		{
			_dbSet.Update(item);
			await _dbContext.SaveChangesAsync();
			return item;
		}

		public virtual async Task<T> DeleteAsync(T item)
		{
			_dbSet.Remove(item);
			await _dbContext.SaveChangesAsync();
			return item;
		}

		public virtual async Task<T> DeleteByIdAsync(long id)
		{
			var itemToBeDeleted = await _dbSet.FindAsync(id);

			if (itemToBeDeleted == null) throw new Exception("Item not found!");

			return await DeleteAsync(itemToBeDeleted);
		}
	}
}
