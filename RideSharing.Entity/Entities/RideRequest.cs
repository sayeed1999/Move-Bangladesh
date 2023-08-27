using RideSharing.Entity.Enums;

namespace RideSharing.Entity.Entities
{
    public class RideRequest
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public string Destination { get; set; }
        public RideStatusEnum Status { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
