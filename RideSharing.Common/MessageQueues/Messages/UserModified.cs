using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.MessageQueues.Messages
{
    public class UserModified
    {
        long Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        short Gender { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string PhoneNumber { get; set; }
        IEnumerable<string> Roles { get; set; }
    }
}
