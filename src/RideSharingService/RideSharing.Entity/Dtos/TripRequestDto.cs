using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity.Dtos
{
    public class TripRequestDto : IRequest<Result<Trip>>
    {
        public long CustomerId { get; set; }
        public long DriverId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
