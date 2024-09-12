﻿using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class PaymentController : BaseController<Payment>
	{
		public PaymentController(IBaseRepository<Payment> repository) : base(repository)
		{

		}
	}
}