using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class DriverAdminController : BaseAdminController<Driver>
	{
		public DriverAdminController(IBaseRepository<Driver> repository) : base(repository)
		{

		}
	}
}
