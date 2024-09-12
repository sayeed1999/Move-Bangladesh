using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class DriverController : BaseController<Driver>
	{
		public DriverController(IBaseRepository<Driver> repository) : base(repository)
		{

		}
	}
}
