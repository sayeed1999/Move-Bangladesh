using RideSharing.Application.Abstractions;
using RideSharing.Domain;
using RideSharing.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Application
{
    public class CustomerRatingRepository : BaseRepository<CustomerRating>, ICustomerRatingRepository
    {
        public CustomerRatingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}