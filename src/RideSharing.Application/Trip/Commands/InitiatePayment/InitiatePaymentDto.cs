using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Trip.Commands.InitiatePayment;

public record struct InitiatePaymentDto(
    long CustomerId,
    long TripId,
    PaymentMethod PaymentMethod) : IRequest<Result<long>>
{
}
