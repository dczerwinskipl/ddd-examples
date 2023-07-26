using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class FlightReservationId : Identifier<FlightReservationId, Guid>
{
    protected FlightReservationId(Guid id) : base(id) { }

    public static FlightReservationId NewId() => new(Guid.NewGuid());

    public override FlightReservationId Copy() => new(Id);
}
