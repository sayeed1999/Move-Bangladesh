using RideSharing.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Payment : Base
    {
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public long Amount { get; set; }
    }
}
