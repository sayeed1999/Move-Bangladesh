using Microsoft.EntityFrameworkCore;
using RideSharing.Application.Abstractions;
using System.Reflection;

namespace RideSharing.Infrastructure.Repositories
{
	// TODO: use primary key type generically
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

		// TODO: use generic pagination here
		public virtual async Task<IEnumerable<T>> FindAllAsync()
		{
			return await _dbSet.ToListAsync();
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

		public virtual async Task<T> UpdateByIdAsync(Guid id, T item)
		{
			Type t = item.GetType();
			PropertyInfo prop = t.GetProperty("Id");
			Guid itemId = (Guid)prop.GetValue(item);
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

		public virtual async Task<T> DeleteByIdAsync(Guid id)
		{
			var itemToBeDeleted = await _dbSet.FindAsync(id);

			if (itemToBeDeleted == null) throw new Exception("Item not found!");

			return await DeleteAsync(itemToBeDeleted);
		}
	}
}
