using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;
using System.Text;

namespace RideSharing.Application
{
	public class TripRepository : BaseRepository<TripEntity>, ITripRepository
	{
		public TripRepository(
			ApplicationDbContext applicationDbContext,
			DapperContext dapperContext)
			: base(applicationDbContext, dapperContext)
		{

		}

		//var unfinishedTrip = await tripRepository.DbSet.FirstOrDefaultAsync(
		//	x => x.TripStatus != TripStatus.TripCompleted);

		public async Task<TripEntity> GetActiveTripForCustomer(Guid customerId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(TripEntity.TripStatus)}\" <> @{nameof(TripEntity.TripStatus)}");
			query.Append($" AND \"{nameof(TripEntity.CustomerId)}\" = @{nameof(TripEntity.CustomerId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripEntity.TripStatus), (int)TripStatus.TripCompleted, System.Data.DbType.Int16);
			parameters.Add(nameof(TripEntity.CustomerId), customerId, System.Data.DbType.Guid);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync<TripEntity>(query.ToString(), parameters);
				return trip;
			}
		}

		public async Task<TripEntity> GetActiveTripForDriver(Guid driverId)
		{
			var query = new StringBuilder();

			query.Append("SELECT * FROM \"Trips\"");
			query.Append($" WHERE \"{nameof(TripEntity.TripStatus)}\" <> @{nameof(TripEntity.TripStatus)}");
			query.Append($" AND \"{nameof(TripEntity.DriverId)}\" = @{nameof(TripEntity.DriverId)}");
			query.Append(" LIMIT 1");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripEntity.TripStatus), (int)TripStatus.TripCompleted, System.Data.DbType.Int16);
			parameters.Add(nameof(TripEntity.DriverId), driverId, System.Data.DbType.Guid);

			using (var connection = _dapperContext.CreateConnection())
			{
				var trip = await connection.QueryFirstOrDefaultAsync(query.ToString(), parameters);
				return trip;
			}
		}
	}
}