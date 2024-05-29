using CSharpFunctionalExtensions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface IUnitOfWork
	{
		#region repositories
		IBaseRepository<CabEntity> CabRepository { get; }
		IBaseRepository<CustomerRatingEntity> CustomerRatingRepository { get; }
		IBaseRepository<CustomerEntity> CustomerRepository { get; }
		IBaseRepository<DriverRatingEntity> DriverRatingRepository { get; }
		IBaseRepository<DriverEntity> DriverRepository { get; }
		IBaseRepository<PaymentEntity> PaymentRepository { get; }
		IBaseRepository<TripLogEntity> TripLogRepository { get; }
		IBaseRepository<TripEntity> TripRepository { get; }
		IBaseRepository<TripRequestLogEntity> TripRequestLogRepository { get; }
		IBaseRepository<TripRequestEntity> TripRequestRepository { get; }
		IUserContext UserContext { get; }
		#endregion

		#region crud helper methods
		Task<Result<int>> SaveChangesAsync();

		public Task<TripRequestEntity> GetActiveTripRequestForCustomer(long customerId);
		public Task<TripRequestEntity> GetActiveTripRequestForDriver(long driverId);
		public Task<TripEntity> GetActiveTripForCustomer(long customerId);
		public Task<TripEntity> GetActiveTripForDriver(long driverId);
		#endregion
	}
}
