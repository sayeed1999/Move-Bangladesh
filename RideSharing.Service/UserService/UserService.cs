using RideSharing.Entity;
using RideSharing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Repository
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> repository) : base(repository)
        {
        }
    }
}
