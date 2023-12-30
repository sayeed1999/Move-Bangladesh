using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class CustomerRatingRepository : BaseRepository<CustomerRating>, ICustomerRatingRepository
	{
		public CustomerRatingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}