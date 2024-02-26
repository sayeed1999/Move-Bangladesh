using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;
using System.Text;

namespace RideSharing.Application
{
	public class TripRepository : BaseRepository<Trip>, ITripRepository
	{
		public TripRepository(
			ApplicationDbContext applicationDbContext,
			DapperContext dapperContext)
			: base(applicationDbContext, dapperContext)
		{

		}

		//var unfinishedTrip = await tripRepository.DbSet.FirstOrDefaultAsync(
		//	x => x.TripStatus != TripStatus.TripCompleted);

		public async Task<Trip> GetActiveTripForCustomer(Guid customerId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(Trip.TripStatus)}\" <> @{nameof(Trip.TripStatus)}");
			query.Append($" AND \"{nameof(Trip.CustomerId)}\" = @{nameof(Trip.CustomerId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(Trip.TripStatus), (int)TripStatus.TripCompleted, System.Data.DbType.Int16);
			parameters.Add(nameof(Trip.CustomerId), customerId, System.Data.DbType.Guid);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
				return trip;
			}
		}

		public async Task<Trip> GetActiveTripForDriver(Guid driverId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(Trip.TripStatus)}\" <> @{nameof(Trip.TripStatus)}");
			query.Append($" AND \"{nameof(Trip.DriverId)}\" = @{nameof(Trip.DriverId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(Trip.TripStatus), TripStatus.TripCompleted, System.Data.DbType.Int16);
			parameters.Add(nameof(Trip.DriverId), driverId, System.Data.DbType.Guid);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
				return trip;
			}
		}
	}
}