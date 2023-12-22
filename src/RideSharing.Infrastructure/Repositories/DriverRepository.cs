using RideSharing.Application.Abstractions;
using RideSharing.Domain;
using RideSharing.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Application
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}