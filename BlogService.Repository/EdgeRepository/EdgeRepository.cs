using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.EdgeRepository
{
    public class EdgeRepository : BaseRepository<Edge>, IEdgeRepository
    {
        public EdgeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Edge> CreateEdgeIfNotExistsAsync(long fromNodeId, long toNodeId, EdgeType edgeType, long currentUserId)
        {
            var edge = await this.FirstOrDefaultAsync(x => x.FromDestinationId == fromNodeId
                                                    && x.ToDestinationId == toNodeId
                                                    && x.EdgeType == edgeType);

            if (edge is null)
            {
                edge = new Edge
                {
                    CreatedById = currentUserId,
                    FromDestinationId = fromNodeId,
                    ToDestinationId = toNodeId,
                    EdgeType = edgeType,
                };
                await this.AddAsync(edge);
            }
            return edge;
        }

        public async Task<Edge> DeleteEdgeIfExistsAsync(long fromNodeId, long toNodeId, EdgeType edgeType)
        {
            var edge = await this.FirstOrDefaultAsync(x => x.FromDestinationId == fromNodeId
                                                    && x.ToDestinationId == toNodeId
                                                    && x.EdgeType == edgeType);

            if (edge is not null)
            {
                await this.DeleteAsync(edge);
            }
            return edge;
        }

    }
}
