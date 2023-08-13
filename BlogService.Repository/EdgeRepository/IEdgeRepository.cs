using BlogService.Entity;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.EdgeRepository
{
    public interface IEdgeRepository : IBaseRepository<Edge>
    {
        Task<Edge> CreateEdgeIfNotExistsAsync(long fromNodeId, long toNodeId, EdgeType edgeType, long currentUserId);
        Task<Edge> DeleteEdgeIfExistsAsync(long fromNodeId, long toNodeId, EdgeType edgeType);
    }
}
