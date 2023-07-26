using DomainDrivenDesign.Core.BuildingBlocks;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Domain.Entities;

public class AirplaneSchema : Entity<AirplaneSchemaId>
{
    public string? AriplaneType { get; set; } // Boeing 747
    public List<AirplaneSeat> Seats { get; set; } = new();
}

public class AirplaneSeat : Entity<AirplaneSeatId>
{
    public SeatClass Class { get; set; }
    public SeatType Type { get; set; }
    public int Row { get; set; }
    public string? Column { get; set; }
}

public enum SeatType
{
    Window,
    Corridor,
    Middle
}

public enum SeatClass
{
    FirstClass,
    EconomicClass
}
