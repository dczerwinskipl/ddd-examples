using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class BookingHolderId : Identifier<BookingHolderId, Guid>
{
    protected BookingHolderId(Guid id) : base(id) { }

    public static BookingHolderId NewId() => new(Guid.NewGuid());

    public override BookingHolderId Copy() => new(Id);
}