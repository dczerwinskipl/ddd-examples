using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class FlightId : Identifier<FlightId, Guid>
{
    protected FlightId(Guid id) : base(id) { }

    public static FlightId NewId() => new(Guid.NewGuid());

    public override FlightId Copy() => new(Id);
}

public class SeatId : Identifier<SeatId, Guid>
{
    protected SeatId(Guid id) : base(id) { }

    public static SeatId NewId() => new(Guid.NewGuid());

    public override SeatId Copy() => new(Id);
}

public class AircraftId : Identifier<AircraftId, Guid>
{
    protected AircraftId(Guid id) : base(id) { }

    public static AircraftId NewId() => new(Guid.NewGuid());

    public override AircraftId Copy() => new(Id);
}

public class FlightReservationId : Identifier<FlightReservationId, Guid>
{
    protected FlightReservationId(Guid id) : base(id) { }

    public static FlightReservationId NewId() => new(Guid.NewGuid());

    public override FlightReservationId Copy() => new(Id);
}
public class SeatAvailabilityId : Identifier<SeatAvailabilityId, Guid>
{
    protected SeatAvailabilityId(Guid id) : base(id) { }

    public static SeatAvailabilityId NewId() => new(Guid.NewGuid());

    public override SeatAvailabilityId Copy() => new(Id);
}

public class SeatsAvailabilityId : Identifier<SeatsAvailabilityId, Guid>
{
    protected SeatsAvailabilityId(Guid id) : base(id) { }

    public static SeatsAvailabilityId NewId() => new(Guid.NewGuid());

    public override SeatsAvailabilityId Copy() => new(Id);
}