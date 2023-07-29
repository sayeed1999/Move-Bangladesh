using BlogService.Entity;
using BlogService.Service.EdgeRepository;
using BlogService.Service.NodeRepository;
using BlogService.Service.UserRepository;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.PostService
{
    public class PostService : BaseService<Node>, IPostService
    {
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Node> nodeRepository;
        private readonly IBaseRepository<Edge> edgeRepository;

        public PostService(
            IUserRepository userRepository,
            INodeRepository nodeRepository,
            IEdgeRepository edgeRepository
        ) : base(nodeRepository)
        {
            this.userRepository = userRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }

    }
}
