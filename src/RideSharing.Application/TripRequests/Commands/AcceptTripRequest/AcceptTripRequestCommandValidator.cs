using System;
using FluentValidation;

namespace RideSharing.Application.TripRequests.Commands.AcceptTripRequest;

public class AcceptTripRequestCommandValidator : AbstractValidator<AcceptTripRequestCommand>
{
	public AcceptTripRequestCommandValidator()
	{
		RuleFor(x => x.DriverId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.TripRequestId).MaximumLength(30).NotEmpty();
	}
}
