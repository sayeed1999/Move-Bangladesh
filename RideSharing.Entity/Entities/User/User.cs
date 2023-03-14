using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class User : IdentityUser<long>
    {
        public User()
        {
            this.CreatedDateUtc = DateTime.UtcNow;
            this.Gender = Gender.Unknown;
        }

        public Gender Gender { get; set; }

        // props I couldn't inherit from Base due to multiple inheritance doesn't support!
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public DateTime? DeletedDateUtc { get; set; }

    }
}
