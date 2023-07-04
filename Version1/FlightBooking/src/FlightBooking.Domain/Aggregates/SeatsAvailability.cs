using System.Diagnostics.CodeAnalysis;
using DomainDrivenDesign.Core.BuildingBlocks;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Domain.Aggregates;

// we dont care about specific place
public class SeatsAvailability : AggregateRoot<SeatsAvailabilityId>
{
    // ResourceId { type, id }
    public FlightId FlightId { get; set; }

    // number of resources
    private int Seats { get; set; }

    private int AvailableSeats => Seats - Reservations.Sum(r => r.Seats);

    private bool IsAvailable => AvailableSeats > 0;

    // owners
    private List<SeatReservation> Reservations { get; set; } = new();

    public void Reserve(FlightReservationId reservationId, int seats)
    {
        if (AvailableSeats < seats)
            throw new DomainException();

        var reservation = Reservations.Find(r => r.ReservationId == reservationId) ?? new(reservationId, 0);
        reservation.Seats += seats;

        // publish
    }

    public void Release(FlightReservationId reservationId)
    {
        var reservation = Reservations.Find(r => r.ReservationId == reservationId) ?? throw new ArgumentException(nameof(reservationId));
        Reservations.Remove(reservation);

        // publish event
    }

    public class SeatReservation : Entity<Guid>
    {
        public FlightReservationId ReservationId { get; internal set; }
        public int Seats { get; internal set; }

        public SeatReservation() { }

        [SetsRequiredMembers]
        public SeatReservation(FlightReservationId reservationId, int seats) : base(Guid.NewGuid())
        {
            ReservationId = reservationId;
            Seats = seats;
        }
    }
}