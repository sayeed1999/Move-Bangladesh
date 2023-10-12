using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Configurations;

public class KeycloakConfig
{
    public string AuthUrl { get; set; }
    public string Tenant { get; set; }
}
