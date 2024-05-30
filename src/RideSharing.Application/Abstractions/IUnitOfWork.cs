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
		ITripRepository TripRepository { get; } // inherited repository
		IBaseRepository<TripRequestLogEntity> TripRequestLogRepository { get; }
		ITripRequestRepository TripRequestRepository { get; } // inherited repository
		IUserContext UserContext { get; }

		#endregion



		#region crud helper methods

		Task<Result<int>> SaveChangesAsync();

		#endregion
	}
}
