using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Contracts.ValueObjects;

public class ReservationId : Identifier<ReservationId, Guid>
{
    protected ReservationId(Guid id) : base(id) { }

    public static ReservationId NewId() => new(Guid.NewGuid());

    public override ReservationId Copy() => new(Id);
}
