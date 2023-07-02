using BlogService.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserService
{
    public class UserService : BaseService<User>
    {
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Node> nodeRepository;
        private readonly IBaseRepository<Edge> edgeRepository;

        public UserService(
            IBaseRepository<User> userRepository,
            IBaseRepository<Node> nodeRepository,
            IBaseRepository<Edge> edgeRepository
        ) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }
    }
}
