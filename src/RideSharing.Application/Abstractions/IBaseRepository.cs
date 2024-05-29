using Microsoft.EntityFrameworkCore.Storage;

namespace RideSharing.Application.Abstractions
{
	public interface IBaseRepository<T> where T : class
	{
		#region transactional helpers
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitTransactionAsync(IDbContextTransaction transaction);
		Task RollBackTransactionAsync(IDbContextTransaction transaction);
		#endregion

		#region crud helpers
		Task<T> FindByIdAsync(long id);
		Task CreateAsync(T item);
		Task BulkCreateAsync(ICollection<T> items);
		void Update(T item);
		void BulkUpdate(ICollection<T> items);
		void Delete(T item);
		void BulkDelete(ICollection<T> items);
		Task<int> SaveChangesAsync();
		#endregion
	}
}
