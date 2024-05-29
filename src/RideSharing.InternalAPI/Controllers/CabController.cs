using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class CabController : BaseController<CabEntity>
	{
		public CabController(IBaseRepository<CabEntity> repository) : base(repository)
		{

		}
	}
}
