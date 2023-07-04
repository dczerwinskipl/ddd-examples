using System.Diagnostics.CodeAnalysis;
using DomainDrivenDesign.Core.BuildingBlocks;
using FlightReservation.DTOs;

namespace FlightReservation.Domain.Aggregates
{
    public class Flight : AggregateRoot<Guid>
    {
        private Guid AirportDepartureId { get; set; }
        private Guid AirportArrivalId { get; set; }
        private DateTimeOffset FlightDeparture { get; set; }
        private DateTimeOffset FlightArrival { get; set; }
        private Guid PlaneId { get; set; }
        private List<Seat> Seats { get; } = new List<Seat>();
        private Flight() { }

        public Flight(Guid flightId, Guid airportDepartureId, Guid airportArrivalId, DateTimeOffset flightDeparture, DateTimeOffset flightArrival, Guid planeId, IEnumerable<SeatDTO> seats) : base(flightId)
        {
            AirportDepartureId = airportDepartureId;
            AirportArrivalId = airportArrivalId;
            FlightDeparture = flightDeparture;
            FlightArrival = flightArrival;
            PlaneId = planeId;
            Seats = seats.Select(Seat.Create).ToList();
        }

        public void ReserveSeat(Guid reservation, IEnumerable<(int Row, string Column)> seats)
        {
            foreach (var (Row, Column) in seats)
            {
                var flightSeat = Seats.Find(s => s.Row == Row && s.Column == Column) ?? throw new ArgumentException(nameof(seats));
                flightSeat.Reserve(reservation);
            }
        }
    }

    public class Seat : Entity<Guid>
    {
        public int Row { get; init; }

        public required string Column { get; init; }

        public SeatType SeatType { get; init; }

        [SetsRequiredMembers]
        protected Seat(Guid id, int row, string column, SeatType seatType) : base(id)
        {
            Row = row;
            Column = column;
            SeatType = seatType;
        }

        private Guid? ReservationId { get; set; }

        private bool IsReserved => ReservationId.HasValue;

        public void Reserve(Guid reservation)
        {
            if (IsReserved)
                throw new DomainException();

            ReservationId = reservation;
        }

        public static Seat Create(Guid id, int row, string column, SeatType seatType) => new(id, row, column, seatType);
        public static Seat Create(SeatDTO seat) => new(Guid.NewGuid(), seat.Row, seat.Column, seat.SeatType);
    }
}
