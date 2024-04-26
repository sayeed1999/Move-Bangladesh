using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class CabRepository : BaseRepository<CabEntity>, ICabRepository
	{
		public CabRepository(ApplicationDbContext applicationDbContext, DapperContext dapperContext) : base(applicationDbContext, dapperContext)
		{
		}
	}
}