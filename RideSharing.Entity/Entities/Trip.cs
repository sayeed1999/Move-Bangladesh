using RideSharing.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Trip : Base
    {
        public long CustomerId { get; set; }
        public long DriverId { get; set; }
        public long PaymentId { get; set; }
        public TripStatus Status { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }

    }
}
