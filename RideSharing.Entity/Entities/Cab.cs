using RideSharing.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Cab : Base
    {
        public string RegNo { get; set; }
        public long DriverId { get; set; }
        public CabType Type { get; set; }
    }
}
