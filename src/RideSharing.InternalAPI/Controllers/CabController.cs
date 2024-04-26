using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class CabController : BaseController<CabEntity>
	{
		public CabController(ICabRepository repository) : base(repository)
		{

		}
	}
}
