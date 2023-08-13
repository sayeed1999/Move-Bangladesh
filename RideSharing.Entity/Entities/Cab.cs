using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Cab : Base
    {
        public string RegNo { get; set; }
        public long DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public CabType Type { get; set; }
    }
}