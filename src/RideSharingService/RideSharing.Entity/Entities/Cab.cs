using RideSharing.Entity.Enums;
using Sayeed.Generic.OnionArchitecture.Entity;

namespace RideSharing.Entity
{
    public class Cab : BaseEntity
    {
        private Cab() : base()
        {
            
        }

        private Cab(string regNo, long driverId, Driver driver, CabType type) : base()
        {
            RegNo = regNo;
            DriverId = driverId;
            Driver = driver;
            Type = type;
        }

        public string RegNo { get; protected set; }
        public long DriverId { get; protected set; }
        public virtual Driver Driver { get; protected set; }
        public CabType Type { get; protected set; }

        public static Cab Create(string regNo, long driverId, Driver driver, CabType type)
        {
            var cab = new Cab(regNo, driverId, driver, type);
            return cab;
        }

    }
}