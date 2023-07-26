using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class FlightAttendantId : Identifier<FlightAttendantId, Guid>
{
    protected FlightAttendantId(Guid id) : base(id) { }

    public static FlightAttendantId NewId() => new(Guid.NewGuid());

    public override FlightAttendantId Copy() => new(Id);
}