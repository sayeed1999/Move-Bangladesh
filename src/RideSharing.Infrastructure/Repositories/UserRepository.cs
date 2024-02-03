using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext applicationDbContext, DapperContext dapperContext) : base(applicationDbContext, dapperContext)
		{

		}
	}
}
