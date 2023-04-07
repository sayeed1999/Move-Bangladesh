using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharing.AuthAPI
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public JWT JWT { get; set; }
        public string ClientUrl { get; set; }
    }

    public class JWT
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
