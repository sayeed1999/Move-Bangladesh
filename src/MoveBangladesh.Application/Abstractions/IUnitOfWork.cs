using CSharpFunctionalExtensions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Abstractions
{
	public interface IUnitOfWork
	{
		#region repositories

		IBaseRepository<Cab> CabRepository { get; }
		IBaseRepository<CustomerRating> CustomerRatingRepository { get; }
		IBaseRepository<Customer> CustomerRepository { get; }
		IBaseRepository<DriverRating> DriverRatingRepository { get; }
		IBaseRepository<Driver> DriverRepository { get; }
		IBaseRepository<Payment> PaymentRepository { get; }
		ITripRepository TripRepository { get; } // inherited repository
		ITripRequestRepository TripRequestRepository { get; } // inherited repository
		IUserContext UserContext { get; }

		#endregion



		#region crud helper methods

		Task<Result<int>> SaveChangesAsync();

		#endregion
	}
}
