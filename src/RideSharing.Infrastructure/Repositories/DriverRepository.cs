using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class DriverRepository : BaseRepository<Driver>, IDriverRepository
	{
		public DriverRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}