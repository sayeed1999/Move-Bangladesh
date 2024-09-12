using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using System.Text;

namespace RideSharing.Persistence.Repositories
{
	public class TripRequestRepository : BaseRepository<TripRequest>, ITripRequestRepository
	{
		public TripRequestRepository(
			ApplicationDbContext dbContext)
			: base(
				  dbContext)
		{
		}

		public async Task<TripRequest> GetActiveTripRequestForCustomer(string customerId)
		{
			// If a trip is requested in less than one minute and it is neither canceled nor started, it is considered an active requested trip.
			// If a trip request has no activity within one minute, it is considered auto-canceled.

			throw new NotImplementedException();

			// DateTime oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

			// var query = new StringBuilder();

			// query.Append($"SELECT * FROM \"TripRequests\"");
			// query.Append($" WHERE (");
			// query.Append($"		(");
			// query.Append($"			\"{nameof(TripRequest.Status)}\" = @{nameof(TripRequest.Status)}");
			// query.Append($"			AND \"{nameof(TripRequest.LastModifiedAt)}\" >= @{nameof(oneMinuteAgo)}");
			// query.Append($"		)");
			// query.Append($"		OR \"{nameof(TripRequest.Status)}\" >= @{nameof(TripRequest.Status)}");
			// query.Append($"	)");
			// query.Append($" AND \"{nameof(TripRequest.CustomerId)}\" = @{nameof(TripRequest.CustomerId)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(TripRequest.Status), (int) TripRequestStatus.NO_DRIVER_FOUND, System.Data.DbType.Int16);
			// parameters.Add(nameof(oneMinuteAgo), oneMinuteAgo, System.Data.DbType.DateTime);
			// parameters.Add(nameof(TripRequest.Status), (int) TripRequestStatus.TRIP_STARTED, System.Data.DbType.Int16);
			// parameters.Add(nameof(TripRequest.CustomerId), customerId, System.Data.DbType.Int64);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var tripRequest = await connection.QueryFirstOrDefaultAsync<TripRequest>(query.ToString(), parameters);
			// 	return tripRequest;
			// }
		}

		public async Task<TripRequest> GetActiveTripRequestForDriver(string driverId)
		{
			throw new NotImplementedException();

			// var query = new StringBuilder();

			// query.Append($"SELECT * FROM \"TripRequests\"");
			// query.Append($"	WHERE \"{nameof(TripRequest.Status)}\" = @{nameof(TripRequest.Status)}");
			// query.Append($" AND \"{nameof(TripRequest.DriverId)}\" = @{nameof(TripRequest.DriverId)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(TripRequest.Status), (int) TripRequestStatus.DRIVER_ACCEPTED, System.Data.DbType.Int16);
			// parameters.Add(nameof(TripRequest.DriverId), driverId, System.Data.DbType.Int64);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var tripRequest = await connection.QueryFirstOrDefaultAsync<TripRequest>(query.ToString(), parameters);
			// 	return tripRequest;
			// }
		}
	}
}
