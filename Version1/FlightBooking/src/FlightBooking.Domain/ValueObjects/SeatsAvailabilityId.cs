using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class SeatsAvailabilityId : Identifier<SeatsAvailabilityId, Guid>
{
    protected SeatsAvailabilityId(Guid id) : base(id) { }

    public static SeatsAvailabilityId NewId() => new(Guid.NewGuid());

    public override SeatsAvailabilityId Copy() => new(Id);
}