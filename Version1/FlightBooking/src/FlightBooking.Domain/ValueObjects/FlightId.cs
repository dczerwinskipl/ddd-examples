using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class FlightId : Identifier<FlightId, Guid>
{
    protected FlightId(Guid id) : base(id) { }

    public static FlightId NewId() => new(Guid.NewGuid());

    public override FlightId Copy() => new(Id);
}
