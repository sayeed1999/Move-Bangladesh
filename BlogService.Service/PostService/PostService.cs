using BlogService.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
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
            IBaseRepository<User> userRepository,
            IBaseRepository<Node> nodeRepository,
            IBaseRepository<Edge> edgeRepository
        ) : base(nodeRepository)
        {
            this.userRepository = userRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }

    }
}
