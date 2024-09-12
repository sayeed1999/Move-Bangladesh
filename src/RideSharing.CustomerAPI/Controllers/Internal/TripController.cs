﻿using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class TripController : BaseController<Trip>
	{
		public TripController(IBaseRepository<Trip> repository) : base(repository)
		{

		}
	}
}