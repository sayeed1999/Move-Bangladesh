using System;
using FluentValidation;

namespace MoveBangladesh.Application.TripRequests.Commands.CancelTripRequest;

public class CancelTripRequestCommandValidator : AbstractValidator<CancelTripRequestCommand>
{
	public CancelTripRequestCommandValidator()
	{
		RuleFor(x => x.CustomerId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.TripRequestId).MaximumLength(30).NotEmpty();
	}
}
