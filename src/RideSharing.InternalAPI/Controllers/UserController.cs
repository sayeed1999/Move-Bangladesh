using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class UserController : BaseController<User>
	{
		public UserController(IUserRepository repository) : base(repository)
		{

		}
	}
}
