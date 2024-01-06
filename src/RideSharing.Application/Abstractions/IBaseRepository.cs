using Microsoft.EntityFrameworkCore;

namespace RideSharing.Application.Abstractions
{
	public interface IBaseRepository<T> where T : class
	{
		public DbSet<T> DbSet { get; }

		public Task<T> FindByIdAsync(Guid id);
		public Task<T> AddAsync(T item);
		public Task<T> UpdateAsync(T item);
		public Task<T> UpdateByIdAsync(long id, T item);
		public Task<T> DeleteAsync(T item);
		public Task<T> DeleteByIdAsync(long id);

		public Task<int> SaveChangesAsync();
	}
}
