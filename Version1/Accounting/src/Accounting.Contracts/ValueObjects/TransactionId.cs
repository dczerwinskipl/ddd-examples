using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class TransactionId : Identifier<TransactionId, Guid>
{
    protected TransactionId(Guid id) : base(id) { }

    public static TransactionId NewId() => new(Guid.NewGuid());

    public override TransactionId Copy() => new(Id);
}
