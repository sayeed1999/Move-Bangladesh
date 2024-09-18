using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;
using System.Text;

namespace MoveBangladesh.Persistence.Repositories
{
	public class TripRequestRepository : BaseRepository<TripRequest>, ITripRequestRepository
	{
		private readonly ILogger<TripRequestRepository> logger;

		public TripRequestRepository(
			ApplicationDbContext dbContext,
			ILogger<TripRequestRepository> logger) : base(dbContext)
		{
			this.logger = logger;
		}

		public async Task<TripRequest?> GetActiveTripRequestForCustomer(string customerId)
		{
			// Conditions for active trip_request request: -
			// 1. driver is found, but trip_request not started yet!
			// 2. driver is not found yet, but trip_request request is last updated is less than one minute ago!

			try
			{
				DateTime oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

				// Method syntax
				// var tripRequest = await _dbSet.SingleOrDefaultAsync(
				// 	x => x.CustomerId == customerId
				// 	&& ((x.Status > TripRequestStatus.NO_DRIVER_FOUND && x.Status < TripRequestStatus.TRIP_STARTED)
				// 	|| (x.Status == TripRequestStatus.NO_DRIVER_FOUND && x.LastModifiedAt < oneMinuteAgo)));

				// Query syntax
				var tripRequest = await (from trip_request in _dbSet
										 where trip_request.CustomerId == customerId
										 && ((trip_request.Status > TripRequestStatus.NO_DRIVER_FOUND && trip_request.Status < TripRequestStatus.TRIP_STARTED)
										 || (trip_request.Status == TripRequestStatus.NO_DRIVER_FOUND && trip_request.LastModifiedAt < oneMinuteAgo))
										 select trip_request).SingleOrDefaultAsync();

				return tripRequest;
			}
			catch (Exception ex)
			{
				logger.LogError($"{nameof(GetActiveTripRequestForCustomer)} threw an exception: {ex}");
				throw;
			}
		}

		public async Task<TripRequest?> GetActiveTripRequestForDriver(string driverId)
		{
			try
			{
				var tripRequest = await (from trip_request in _dbSet
										 where trip_request.DriverId == driverId
										 && trip_request.Status > TripRequestStatus.NO_DRIVER_FOUND
										 && trip_request.Status < TripRequestStatus.TRIP_STARTED
										 select trip_request).SingleOrDefaultAsync();

				return tripRequest;
			}
			catch (Exception ex)
			{
				logger.LogError($"{nameof(GetActiveTripRequestForDriver)} threw an exception: {ex}");
				throw;
			}
		}
	}
}
