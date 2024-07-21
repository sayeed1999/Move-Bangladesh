using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using System.Data;
using System.Text;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRepository : BaseRepository<TripEntity>, ITripRepository
	{
		public TripRepository(
			ApplicationDbContext dbContext,
			DapperContext dapperContext)
			: base(
				  dbContext,
				  dapperContext)
		{
		}

		public async Task<TripEntity> GetActiveTripForCustomer(long customerId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(TripEntity.TripStatus)}\" <> @{nameof(TripEntity.TripStatus)}");
			query.Append($" AND \"{nameof(TripEntity.CustomerId)}\" = @{nameof(TripEntity.CustomerId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripEntity.TripStatus), (int)TripStatus.PAYMENT_COMPLETED, DbType.Int16);
			parameters.Add(nameof(TripEntity.CustomerId), customerId, DbType.Int64);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync<TripEntity>(query.ToString(), parameters);
				return trip;
			}
		}

		public async Task<TripEntity> GetActiveTripForDriver(long driverId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(TripEntity.TripStatus)}\" <> @{nameof(TripEntity.TripStatus)}");
			query.Append($" AND \"{nameof(TripEntity.DriverId)}\" = @{nameof(TripEntity.DriverId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripEntity.TripStatus), (int)TripStatus.PAYMENT_COMPLETED, System.Data.DbType.Int16);
			parameters.Add(nameof(TripEntity.DriverId), driverId, System.Data.DbType.Int64);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
				return trip;
			}
		}

		public async Task<TripEntity> HasOngoingTrip(long tripId, long driverId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(TripEntity.Id)}\" = @{nameof(TripEntity.Id)}");
			query.Append($" AND \"{nameof(TripEntity.DriverId)}\" = @{nameof(TripEntity.DriverId)}");
			query.Append($" AND \"{nameof(TripEntity.TripStatus)}\" = @{nameof(TripEntity.TripStatus)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripEntity.Id), tripId, DbType.Int64);
			parameters.Add(nameof(TripEntity.DriverId), driverId, DbType.Int64);
			parameters.Add(nameof(TripEntity.TripStatus), (int)TripStatus.ONGOING, DbType.Int16);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
				return trip;
			}	
		}
	}
}
