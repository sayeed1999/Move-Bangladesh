using RideSharing.Common.Enums;
using RideSharing.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public abstract class Human : Base
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public Gender Gender { get; set; } = Gender.Unknown;
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? IsVerified { get; set; }
    }
}
