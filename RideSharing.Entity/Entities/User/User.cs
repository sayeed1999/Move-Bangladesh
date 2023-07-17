using Microsoft.AspNetCore.Identity;
using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}