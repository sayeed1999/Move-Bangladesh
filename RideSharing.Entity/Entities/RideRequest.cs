using System.ComponentModel.DataAnnotations;
using RideSharing.Entity.Enums;

namespace RideSharing.Entity.Entities
{
    public class RideRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public RideStatusEnum Status { get; set; }

        [Required]
        public DateTime RequestTime { get; set; }

        public Driver Driver { get; set; }
    }
}
