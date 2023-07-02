using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        public PaymentService(IBaseRepository<Payment> baseRepository) : base(baseRepository)
        {
        }
    }
}
