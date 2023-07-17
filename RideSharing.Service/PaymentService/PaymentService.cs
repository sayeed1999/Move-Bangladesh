using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        public PaymentService(IBaseRepository<Payment> baseRepository) : base(baseRepository)
        {
        }
    }
}