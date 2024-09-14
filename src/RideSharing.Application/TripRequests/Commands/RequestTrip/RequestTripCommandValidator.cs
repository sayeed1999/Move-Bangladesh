using System;
using FluentValidation;

namespace RideSharing.Application.TripRequests.Commands.TripRequests;

public class RequestTripCommandValidator : AbstractValidator<RequestTripCommand>
{
	public RequestTripCommandValidator()
	{
		RuleFor(x => x.CustomerId).MaximumLength(30).NotEmpty();
	}
}
