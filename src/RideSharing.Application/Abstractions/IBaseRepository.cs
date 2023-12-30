using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RideSharing.Application.Abstractions
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
		public Task<T> FindByIdAsync(Guid id);
		public Task<T> AddAsync(T item);
		public Task<T> UpdateAsync(T item);
		public Task<T> UpdateByIdAsync(long id, T item);
		public Task<T> DeleteAsync(T item);
		public Task<T> DeleteByIdAsync(long id);

		public Task<int> SaveChangesAsync();
	}
}
