using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity
{
    public class Base
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; } 
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDateUtc { get; set; }
    }
}
