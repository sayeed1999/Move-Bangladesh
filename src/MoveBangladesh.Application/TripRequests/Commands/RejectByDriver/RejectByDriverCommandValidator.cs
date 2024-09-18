using System;
using FluentValidation;

namespace MoveBangladesh.Application.TripRequests.Commands.RejectByDriver;

public class RejectByDriverCommandValidator : AbstractValidator<RejectByDriverCommand>
{
	public RejectByDriverCommandValidator()
	{
		RuleFor(x => x.DriverId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.TripRequestId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.Reason).MaximumLength(60);
	}
}
