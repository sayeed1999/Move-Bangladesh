using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Application
{
    public class CabService : BaseService<Cab>, ICabService
    {
        public CabService(IBaseRepository<Cab> repository) : base(repository)
        {
        }
    }
}