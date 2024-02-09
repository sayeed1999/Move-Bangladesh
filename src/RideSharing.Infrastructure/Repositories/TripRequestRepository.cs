using Dapper;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;
using System.Text;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRequestRepository : BaseRepository<TripRequest>, ITripRequestRepository
	{
		public TripRequestRepository(
			ApplicationDbContext applicationDbContext,
			DapperContext dapperContext)
			: base(applicationDbContext, dapperContext)
		{

		}

		public async Task<TripRequest> GetActiveTripRequestForCustomer(Guid customerId)
		{
			// If a trip is requested in less than one minute and it is neither canceled nor started, it is considered an active requested trip.
			// If a trip request has no activity within one minute, it is considered auto-canceled.

			DateTime oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

			var query = new StringBuilder();

			query.Append($"SELECT TOP 1 FROM TripRequests");
			query.Append($" WHERE {nameof(TripRequest.Status)} = @{nameof(TripRequest.Status)}");
			query.Append($" AND {nameof(TripRequest.UpdatedAt)} >= @{nameof(oneMinuteAgo)}");
			query.Append($" AND {nameof(TripRequest.CustomerId)} = @{nameof(TripRequest.CustomerId)}");

			var parameters = new DynamicParameters();

			parameters.Add(nameof(TripRequest.Status), TripRequestStatus.NoDriverAccepted, System.Data.DbType.Int16);
			parameters.Add(nameof(oneMinuteAgo), oneMinuteAgo, System.Data.DbType.DateTime);
			parameters.Add(nameof(TripRequest.CustomerId), customerId, System.Data.DbType.Guid);

			using (var connection = _dapperContext.CreateConnection())
			{
				var tripRequest = await connection.QueryFirstOrDefaultAsync<TripRequest>(query.ToString(), parameters);
				return tripRequest;
			}
		}

	}
}
