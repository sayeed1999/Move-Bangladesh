using RideSharing.Entity;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> userRepository;
        
        public UserService( 
            IBaseRepository<User> userRepository
        ) : base(userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}