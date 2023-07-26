using DomainDrivenDesign.Core.BuildingBlocks;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Domain.Entities;

public class Flight : Entity<FlightId>
{
    public AirplaneSchemaId? AirplaneSchemaId { get; set; }
}
