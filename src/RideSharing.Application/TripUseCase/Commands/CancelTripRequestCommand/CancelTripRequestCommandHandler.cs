﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripUseCase.Commands.CancelTripRequestCommand
{
	public class CancelTripRequestCommandHandler
		: IRequestHandler<CancelTripRequestCommandDto, Result<CancelTripRequestCommandResponseDto>>
	{
		private readonly ITripRequestRepository tripRequestRepository;
		private readonly ITripRequestLogRepository tripRequestLogRepository;
		private readonly ICustomerRepository customerRepository;

		public CancelTripRequestCommandHandler(
			ITripRequestRepository tripRequestRepository,
			ITripRequestLogRepository tripRequestLogRepository,
			ICustomerRepository customerRepository)
		{
			this.tripRequestRepository = tripRequestRepository;
			this.tripRequestLogRepository = tripRequestLogRepository;
			this.customerRepository = customerRepository;
		}

		public async Task<Result<CancelTripRequestCommandResponseDto>> Handle(CancelTripRequestCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await this.customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<CancelTripRequestCommandResponseDto>("Customer is not found.");
			}

			// Step 2: check trip request exists
			DateTime oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

			var requestedTrip = await this.tripRequestRepository.DbSet.FirstOrDefaultAsync(
				x => x.Status == TripRequestStatus.NoDriverAccepted
					&& x.UpdatedAt >= oneMinuteAgo);

			if (requestedTrip == null)
			{
				return Result.Failure<CancelTripRequestCommandResponseDto>("Customer has no pending requested trip.");
			}

			// Step 3: prepare domain entity
			Result<TripRequest> canceledTripRequest = TripRequest.Cancel(requestedTrip);

			// Step 4: perform database operations

			var transaction = await this.tripRequestRepository.BeginTransactionAsync();

			TripRequest res;

			try
			{
				res = await this.tripRequestRepository.UpdateAsync(canceledTripRequest.Value);

				await this.tripRequestLogRepository.AddAsync(new TripRequestLog(res));

				await this.tripRequestRepository.CommitTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new CancelTripRequestCommandResponseDto(true);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await this.tripRequestRepository.RollBackTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new CancelTripRequestCommandResponseDto(true);

				return Result.Failure<CancelTripRequestCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
