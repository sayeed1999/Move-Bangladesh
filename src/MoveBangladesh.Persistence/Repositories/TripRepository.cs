using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;
using System.Data;
using System.Text;

namespace MoveBangladesh.Persistence.Repositories
{
	public class TripRepository : BaseRepository<Trip>, ITripRepository
	{
		private readonly ILogger<TripRepository> logger;

		public TripRepository(
			ApplicationDbContext dbContext,
			ILogger<TripRepository> logger)
			: base(dbContext)
		{
			this.logger = logger;
		}

		public async Task<Trip?> GetActiveTripForCustomer(string customerId)
		{
			try
			{
				var entity = await (from trip in _dbSet
									where trip.CustomerId == customerId
									&& trip.TripStatus < TripStatus.PAYMENT_COMPLETED
									select trip).SingleOrDefaultAsync();

				return entity;
			}
			// Step 1: Catch the Exception: Use a 'try-catch' block to handle the exception.
			catch (Exception ex)
			{
				// Step 2: Log the Exception: Ensure all relevant details (like the
				// exception message and stack trace) are captured for troubleshooting.
				logger.LogError($"{nameof(GetActiveTripForCustomer)} threw an exception: {ex}");

				// Step 3: Re-Throw: Re-throw the exception without losing the original
				// stack trace to allow higher-level handling.
				throw;
			}
		}

		public async Task<Trip?> GetActiveTripForDriver(string driverId)
		{
			try
			{
				var entity = await (from trip in _dbSet
									where trip.DriverId == driverId
									&& trip.TripStatus < TripStatus.PAYMENT_COMPLETED
									select trip).SingleOrDefaultAsync();

				return entity;
			}
			catch (Exception ex)
			{
				logger.LogError($"{nameof(GetActiveTripForDriver)} threw an exception: {ex}");
				throw;
			}
		}

		public async Task<Trip?> GetTripForCustomerWithPendingPayment(string tripId, string customerId)
		{
			try
			{
				var entity = await (from trip in _dbSet
									where trip.CustomerId == customerId
									&& trip.TripStatus == TripStatus.WAITING_FOR_PAYMENT
									select trip).SingleOrDefaultAsync();

				return entity;

			}
			catch (Exception ex)
			{
				logger.LogError($"{nameof(GetTripForCustomerWithPendingPayment)} threw an exception: {ex}");
				throw;
			}
		}
	}
}
