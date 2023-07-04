using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace FlightBooking.Domain.Entities
{
    public class Airplane : Entity<AirplaneId>
    {
    }

    public class Seat : Entity<SeatId>
    {
    }

    public class FlightInfo : Entity<FlightInfoId>
    {
        public FlightId FlightId { get; set; }
    }
}
