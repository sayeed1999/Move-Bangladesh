using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class CustomerRatingRepository : BaseRepository<CustomerRatingEntity>, ICustomerRatingRepository
	{
		public CustomerRatingRepository(ApplicationDbContext applicationDbContext, DapperContext dapperContext) : base(applicationDbContext, dapperContext)
		{
		}
	}
}