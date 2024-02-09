﻿using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	public class TripLogRepository : BaseRepository<TripLog>, ITripLogRepository
	{
		public TripLogRepository(
			ApplicationDbContext applicationDbContext,
			DapperContext dapperContext)
			: base(applicationDbContext, dapperContext)
		{

		}
	}
}
