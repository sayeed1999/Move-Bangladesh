using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Trips.Commands.InitiatePayment;

public record struct InitiatePaymentDto(
    string CustomerId,
    string TripId,
    PaymentMethod PaymentMethod) : IRequest<Result<string>>
{
}
