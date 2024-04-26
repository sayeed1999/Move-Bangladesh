using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class DriverRatingRepository : BaseRepository<DriverRatingEntity>, IDriverRatingRepository
	{
		public DriverRatingRepository(ApplicationDbContext applicationDbContext, DapperContext dapperContext) : base(applicationDbContext, dapperContext)
		{
		}
	}
}