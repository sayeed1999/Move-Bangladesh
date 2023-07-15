using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.MessageQueues.Messages
{
    public class UserRegistered
    {
        public long Id { get; set; }
        public long FirstName { get; set; }
        public long LastName { get; set; }
        public long Gender { get; set; }
        public long Email { get; set; }
        public long UserName { get; set; }
        public long PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
