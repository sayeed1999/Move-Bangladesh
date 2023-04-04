using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharing.API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string ClientUrl { get; set; }
    }
}
