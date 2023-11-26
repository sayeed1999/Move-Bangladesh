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
        private TripQueryDto() { }

        public static TripQueryDto Create(int tripId)
        {
            return new TripQueryDto()
            {
                TripId = tripId,
            };
        }

        public long TripId { get; private set; }
    }
}
