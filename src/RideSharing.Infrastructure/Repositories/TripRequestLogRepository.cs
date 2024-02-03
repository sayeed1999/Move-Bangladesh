using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRequestLogRepository : BaseRepository<TripRequestLog>, ITripRequestLogRepository
	{
		public TripRequestLogRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{

		}
	}
}
