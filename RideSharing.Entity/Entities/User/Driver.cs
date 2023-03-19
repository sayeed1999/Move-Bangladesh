using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Driver : Human
    {
        public virtual ICollection<CustomerRating> CustomerRatings { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
