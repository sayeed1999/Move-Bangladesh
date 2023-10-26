using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity.Dtos
{
    public class TripQueryDto : IRequest<Result<Trip>>
    {
        public long TripId { get; set; }
    }
}
