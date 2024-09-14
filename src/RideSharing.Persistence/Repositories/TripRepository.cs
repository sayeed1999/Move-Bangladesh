using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using System.Data;
using System.Text;

namespace RideSharing.Persistence.Repositories
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
			catch (Exception ex)
			{
				logger.LogError($"{nameof(GetActiveTripForCustomer)} threw an exception: {ex}");
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

		public async Task<Trip> HasOngoingTrip(string tripId, string driverId)
		{
			throw new NotImplementedException();

			// var query = new StringBuilder();

			// query.Append("SELECT * FROM \"Trips\"");
			// query.Append($" WHERE \"{nameof(Trip.Id)}\" = @{nameof(Trip.Id)}");
			// query.Append($" AND \"{nameof(Trip.DriverId)}\" = @{nameof(Trip.DriverId)}");
			// query.Append($" AND \"{nameof(Trip.TripStatus)}\" = @{nameof(Trip.TripStatus)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(Trip.Id), tripId, DbType.Int64);
			// parameters.Add(nameof(Trip.DriverId), driverId, DbType.Int64);
			// parameters.Add(nameof(Trip.TripStatus), (int) TripStatus.ONGOING, DbType.Int16);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
			// 	return trip;
			// }
		}

		public async Task<Trip> HasTripWaitingForPayment(string tripId, string customerId)
		{
			throw new NotImplementedException();

			// var query = new StringBuilder();

			// query.Append("SELECT * FROM \"Trips\"");
			// query.Append($" WHERE \"{nameof(Trip.Id)}\" = @{nameof(Trip.Id)}");
			// query.Append($" AND \"{nameof(Trip.CustomerId)}\" = @{nameof(Trip.CustomerId)}");
			// query.Append($" AND \"{nameof(Trip.TripStatus)}\" = @{nameof(Trip.TripStatus)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(Trip.Id), tripId, DbType.Int64);
			// parameters.Add(nameof(Trip.CustomerId), customerId, DbType.Int64);
			// parameters.Add(nameof(Trip.TripStatus), (int) TripStatus.WAITING_FOR_PAYMENT, DbType.Int16);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
			// 	return trip;
			// }
		}
	}
}
