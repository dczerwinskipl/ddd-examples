using DomainDrivenDesign.Core.BuildingBlocks;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Domain.Aggregates;

// we care about specific place
public class SeatAvailability : AggregateRoot<SeatAvailabilityId>
{
    // ResourceId { type, id }
    public FlightId? FlightId { get; set; }
    public AirplaneSeatId? SeatId { get; set; }

    private bool IsAvailable => ReservationId is null;

    // owner of resource, allow overbook?
    private FlightReservationId? ReservationId { get; set; }

    public void Reserve(FlightReservationId reservationId)
    {
        if (IsAvailable)
            throw new DomainException();

        ReservationId = reservationId;
        // publish
    }

    public void Release(FlightReservationId reservationId)
    {
        if (ReservationId != reservationId)
            throw new ArgumentException(nameof(reservationId));

        ReservationId = null;
        // publish event
    }
}
