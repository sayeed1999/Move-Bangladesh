using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRequestLogRepository : BaseRepository<TripRequestLogEntity>, ITripRequestLogRepository
	{
		public TripRequestLogRepository(
			ApplicationDbContext applicationDbContext,
			DapperContext dapperContext)
			: base(applicationDbContext, dapperContext)
		{

		}
	}
}
