﻿using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.DriverCancelTripCommand
{
	public class DriverCancelTripCommandHandler(
		ITripRepository tripRepository,
		ITripLogRepository tripLogRepository,
		IDriverRepository driverRepository)
		: IRequestHandler<DriverCancelTripCommandDto, Result<DriverCancelTripCommandResponseDto>>
	{
		public async Task<Result<DriverCancelTripCommandResponseDto>> Handle(DriverCancelTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check driver exists
			var driverInDB = await driverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<DriverCancelTripCommandResponseDto>("Driver is not found.");
			}

			// Step 2: check trip request exists
			var activeTrip = await tripRepository.GetActiveTripForDriver(request.DriverId);

			if (activeTrip == null)
			{
				return Result.Failure<DriverCancelTripCommandResponseDto>("Driver has no active trip.");
			}

			// Step 3: prepare entity
			var modifiedTripResult = Trip.CancelByDriver(activeTrip);

			if (modifiedTripResult.IsFailure)
			{
				return Result.Failure<DriverCancelTripCommandResponseDto>(modifiedTripResult.Error);
			}

			var modifiedTrip = modifiedTripResult.Value;

			// Step 4: perform database operations

			var transaction = await tripRepository.BeginTransactionAsync();

			Trip res;

			try
			{
				res = await tripRepository.UpdateAsync(modifiedTrip);

				await tripLogRepository.AddAsync(new TripLog(res));

				await tripRepository.CommitTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new DriverCancelTripCommandResponseDto(request.DriverId, request.TripId, request.Reason);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await tripRepository.RollBackTransactionAsync(transaction);

				return Result.Failure<DriverCancelTripCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
