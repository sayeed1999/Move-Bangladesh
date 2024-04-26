using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class DriverController : BaseController<DriverEntity>
	{
		public DriverController(IDriverRepository repository) : base(repository)
		{

		}
	}
}
