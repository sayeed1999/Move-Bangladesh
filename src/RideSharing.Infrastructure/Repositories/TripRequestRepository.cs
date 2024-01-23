using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripRequestRepository : BaseRepository<TripRequest>, ITripRequestRepository
	{
		public TripRequestRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{

		}
	}

}
