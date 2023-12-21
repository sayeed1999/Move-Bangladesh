using RideSharing.Application.Abstractions;
using RideSharing.Domain;
using RideSharing.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Application
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}