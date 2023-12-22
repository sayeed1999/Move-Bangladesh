using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
    public class TripRequestCommandDto : IRequest<Result<TripRequestCommandResponseDto>>
    {
        public long CustomerId { get; set; }
        public long DriverId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
