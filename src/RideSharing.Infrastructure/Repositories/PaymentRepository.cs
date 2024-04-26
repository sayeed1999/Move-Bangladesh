using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Application
{
	public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
	{
		public PaymentRepository(ApplicationDbContext applicationDbContext, DapperContext dapperContext) : base(applicationDbContext, dapperContext)
		{
		}
	}
}