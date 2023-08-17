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

        public Gender Gender { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public bool? IsVerified { get; private set; }
    }
}