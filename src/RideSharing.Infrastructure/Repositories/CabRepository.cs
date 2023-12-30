using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class CabRepository : BaseRepository<Cab>, ICabRepository
	{
		public CabRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}