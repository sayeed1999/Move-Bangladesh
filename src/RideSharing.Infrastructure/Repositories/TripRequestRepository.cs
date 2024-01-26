using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRequestRepository : BaseRepository<TripRequest>, ITripRequestRepository
	{
		public TripRequestRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{

		}

		public override Task<TripRequest> AddAsync(TripRequest item)
		{
			// With any insert operation, the log will be created!
			this._dbContext.TripRequestLogs.Add(new TripRequestLog(item));

			return base.AddAsync(item);
		}

		public override Task<TripRequest> UpdateAsync(TripRequest item)
		{
			// With any update operation, the log will be created!
			this._dbContext.TripRequestLogs.Add(new TripRequestLog(item));

			return base.UpdateAsync(item);
		}
	}
}
