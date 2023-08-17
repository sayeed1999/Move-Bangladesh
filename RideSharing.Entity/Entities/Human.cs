using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public abstract class Human : Base
    {
        public Gender Gender { get; set; } = Gender.Unknown;
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? IsVerified { get; set; }
    }
}