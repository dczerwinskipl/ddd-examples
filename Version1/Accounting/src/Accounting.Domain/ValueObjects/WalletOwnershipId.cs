using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletOwnershipId : Identifier<WalletOwnershipId, Guid>
{
    protected WalletOwnershipId(Guid id) : base(id) { }

    public static WalletOwnershipId NewId() => new(Guid.NewGuid());

    public override WalletOwnershipId Copy() => new(Id);
}
