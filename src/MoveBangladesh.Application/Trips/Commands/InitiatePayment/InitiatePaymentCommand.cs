using CSharpFunctionalExtensions;
using MediatR;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Trips.Commands.InitiatePayment;

public record struct InitiatePaymentCommand(
	string CustomerId,
	string TripId,
	PaymentMethod PaymentMethod) : IRequest<Result<string>>
{
}
