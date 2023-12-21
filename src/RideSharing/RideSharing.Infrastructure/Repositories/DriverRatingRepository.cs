using RideSharing.Application.Abstractions;
using RideSharing.Domain;
using RideSharing.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Application
{
    public class DriverRatingRepository : BaseRepository<DriverRating>, IDriverRatingRepository
    {
        public DriverRatingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}