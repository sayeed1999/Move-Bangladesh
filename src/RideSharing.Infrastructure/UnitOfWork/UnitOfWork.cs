using CSharpFunctionalExtensions;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.UnitOfWork
{
	public partial class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _efcoreContext { get; init; }
		private IDapperContext _dapperContext { get; init; }

		public UnitOfWork(
			ApplicationDbContext efcoreContext,
			IDapperContext dapperContext,
			IBaseRepository<CabEntity> cabRepository,
			IBaseRepository<CustomerRatingEntity> customerRatingRepository,
			IBaseRepository<CustomerEntity> customerRepository,
			IBaseRepository<DriverRatingEntity> driverRatingRepository,
			IBaseRepository<DriverEntity> driverRepository,
			IBaseRepository<PaymentEntity> paymentRepository,
			IBaseRepository<TripLogEntity> tripLogRepository,
			ITripRepository tripRepository,
			IBaseRepository<TripRequestLogEntity> tripRequestLogRepository,
			ITripRequestRepository tripRequestRepository,
			IUserContext userContext
			)
		{
			_efcoreContext = efcoreContext;
			_dapperContext = dapperContext;

			CabRepository = cabRepository;
			CustomerRatingRepository = customerRatingRepository;
			CustomerRepository = customerRepository;
			DriverRatingRepository = driverRatingRepository;
			DriverRepository = driverRepository;
			PaymentRepository = paymentRepository;
			TripLogRepository = tripLogRepository;
			TripRepository = tripRepository;
			TripRequestLogRepository = tripRequestLogRepository;
			TripRequestRepository = tripRequestRepository;

			UserContext = userContext;
		}

		public IBaseRepository<CabEntity> CabRepository { get; }
		public IBaseRepository<CustomerRatingEntity> CustomerRatingRepository { get; }
		public IBaseRepository<CustomerEntity> CustomerRepository { get; }
		public IBaseRepository<DriverRatingEntity> DriverRatingRepository { get; }
		public IBaseRepository<DriverEntity> DriverRepository { get; }
		public IBaseRepository<PaymentEntity> PaymentRepository { get; }
		public IBaseRepository<TripLogEntity> TripLogRepository { get; }
		public ITripRepository TripRepository { get; }
		public IBaseRepository<TripRequestLogEntity> TripRequestLogRepository { get; }
		public ITripRequestRepository TripRequestRepository { get; }
		public IUserContext UserContext { get; }

		public async Task<Result<int>> SaveChangesAsync()
		{
			try
			{
				return await _efcoreContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return Result.Failure<int>($"Failed with error: {ex.Message}");
			}
		}
	}
}
