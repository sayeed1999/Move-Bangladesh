using BlogService.Entity;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.CommentService
{
    public class CommentService : BaseService<Node>, ICommentService
    {
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Node> nodeRepository;
        private readonly IBaseRepository<Edge> edgeRepository;

        public CommentService(
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
