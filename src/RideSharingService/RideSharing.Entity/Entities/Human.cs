using RideSharing.Common.Enums;
using Sayeed.Generic.OnionArchitecture.Entity;

namespace RideSharing.Entity
{
    public abstract class Human : BaseEntity
    {
        public Human() : base()
        {
            Gender = Gender.Unknown;
            IsVerified = false;
        }

        public long AuthUserId { get; protected set; }
        public string UserName { get; set; }
        public Gender Gender { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public string Phone { get; protected set; }
        public string Email { get; protected set; }
        public bool? IsVerified { get; protected set; }
    }
}