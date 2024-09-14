using System;
using FluentValidation;

namespace RideSharing.Application.TripRequests.Commands.RejectByCustomer;

public class RejectByCustomerCommandValidator : AbstractValidator<RejectByCustomerCommand>
{
	public RejectByCustomerCommandValidator()
	{
		RuleFor(x => x.CustomerId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.TripRequestId).MaximumLength(30).NotEmpty();
		RuleFor(x => x.Reason).MaximumLength(60);
	}
}
