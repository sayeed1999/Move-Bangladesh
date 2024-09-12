using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using System.Data;
using System.Text;

namespace RideSharing.Persistence.Repositories
{
	public class TripRepository : BaseRepository<Trip>, ITripRepository
	{
		public TripRepository(
			ApplicationDbContext dbContext)
			: base(
				  dbContext)
		{
		}

		public async Task<Trip> GetActiveTripForCustomer(string customerId)
		{
			throw new NotImplementedException();

			// var query = new StringBuilder();

			// query.Append("SELECT * FROM \"Trips\"");
			// query.Append($" WHERE \"{nameof(Trip.TripStatus)}\" <> @{nameof(Trip.TripStatus)}");
			// query.Append($" AND \"{nameof(Trip.CustomerId)}\" = @{nameof(Trip.CustomerId)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(Trip.TripStatus), (int) TripStatus.PAYMENT_COMPLETED, DbType.Int16);
			// parameters.Add(nameof(Trip.CustomerId), customerId, DbType.Int64);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var trip = await connection.QueryFirstOrDefaultAsync<Trip>(query.ToString(), parameters);
			// 	return trip;
			// }
		}

		public async Task<Trip> GetActiveTripForDriver(string driverId)
		{
			throw new NotImplementedException();

			// var query = new StringBuilder();

			// query.Append("SELECT * FROM \"Trips\"");
			// query.Append($" WHERE \"{nameof(Trip.TripStatus)}\" <> @{nameof(Trip.TripStatus)}");
			// query.Append($" AND \"{nameof(Trip.DriverId)}\" = @{nameof(Trip.DriverId)}");
			// query.Append(" LIMIT 1");

			// var parameters = new DynamicParameters();

			// parameters.Add(nameof(Trip.TripStatus), (int) TripStatus.PAYMENT_COMPLETED, System.Data.DbType.Int16);
			// parameters.Add(nameof(Trip.DriverId), driverId, System.Data.DbType.Int64);

			// using (var connection = _dapperContext.CreateConnection())
			// {
			// 	var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
			// 	return trip;
			// }
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
