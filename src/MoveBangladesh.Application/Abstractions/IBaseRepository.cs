using Microsoft.EntityFrameworkCore.Storage;

namespace MoveBangladesh.Application.Abstractions
{
	public interface IBaseRepository<T> where T : class
	{
		#region transactional helpers
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitTransactionAsync(IDbContextTransaction transaction);
		Task RollBackTransactionAsync(IDbContextTransaction transaction);
		#endregion

		#region crud helpers
		Task<T?> FindByIdAsync(string id);
		Task CreateAsync(T item);
		Task BulkCreateAsync(ICollection<T> items);
		void Update(T item);
		void BulkUpdate(ICollection<T> items);
		void SoftDelete(T item);
		void BulkSoftDelete(ICollection<T> items);
		void HardDelete(T item);
		void BulkHardDelete(ICollection<T> items);
		Task<int> SaveChangesAsync();
		#endregion
	}
}
