using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Cab : Base
    {
        public string RegNo { get; private set; }
        public long DriverId { get; private set; }
        public virtual Driver Driver { get; private set; }
        public CabType Type { get; private set; }
    }
}