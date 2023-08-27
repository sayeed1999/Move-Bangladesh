using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public abstract class Human : Base
    {
        public Human() : base()
        {
            Gender = Gender.Unknown;
            IsVerified = false;
        }

        public Gender Gender { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public string Phone { get; protected set; }
        public string Email { get; protected set; }
        public bool? IsVerified { get; protected set; }
    }
}