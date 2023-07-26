using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class SeatAvailabilityId : Identifier<SeatAvailabilityId, Guid>
{
    protected SeatAvailabilityId(Guid id) : base(id) { }

    public static SeatAvailabilityId NewId() => new(Guid.NewGuid());

    public override SeatAvailabilityId Copy() => new(Id);
}
