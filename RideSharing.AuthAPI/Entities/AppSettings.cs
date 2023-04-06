using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharing.AuthAPI
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public string ClientUrl { get; set; }
    }
}
