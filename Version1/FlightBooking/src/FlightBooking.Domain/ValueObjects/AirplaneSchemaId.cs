using DomainDrivenDesign.Core.ValueObjects;

namespace FlightBooking.Domain.ValueObjects;

public class AirplaneSchemaId : Identifier<AirplaneSchemaId, Guid>
{
    protected AirplaneSchemaId(Guid id) : base(id) { }

    public static AirplaneSchemaId NewId() => new(Guid.NewGuid());

    public override AirplaneSchemaId Copy() => new(Id);
}
