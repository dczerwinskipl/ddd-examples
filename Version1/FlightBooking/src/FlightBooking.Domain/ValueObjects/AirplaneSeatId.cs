using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class AirplaneSeatId : Identifier<AirplaneSeatId, Guid>
{
    protected AirplaneSeatId(Guid id) : base(id) { }

    public static AirplaneSeatId NewId() => new(Guid.NewGuid());

    public override AirplaneSeatId Copy() => new(Id);
}
