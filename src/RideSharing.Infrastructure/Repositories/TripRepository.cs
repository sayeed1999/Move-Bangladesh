using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class TripRepository : BaseRepository<Trip>, ITripRepository
	{
		public TripRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{

		}
	}
}