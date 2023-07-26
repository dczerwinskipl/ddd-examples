using DomainDrivenDesign.Core.BuildingBlocks;
using DomainDrivenDesign.Core.ValueObjects;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Domain.Aggregates;

public class FlightReservation : AggregateRoot<FlightReservationId>
{
    public BookingHolderId? BookingHolderId { get; private set; }
    public FlightId? FlightId { get; private set; }
    private Money? Price { get; set; }
    private List<FlightAttendant> Attendants { get; set; } = new();
}

public class FlightAttendant : Entity<FlightAttendantId>
{
    private string? Name { get; set; }
    private DocumentType DocumentType { get; set; }
    private string? DocumentNumber { get; set; }
}

public enum DocumentType
{
    Passport, ID
}
