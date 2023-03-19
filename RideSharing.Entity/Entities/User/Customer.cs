using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Customer : Human
    {
        public virtual ICollection<DriverRating> DriverRatings { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
