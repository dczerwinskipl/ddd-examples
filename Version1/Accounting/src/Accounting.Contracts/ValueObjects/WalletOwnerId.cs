using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletOwnerId : Identifier<WalletOwnerId, Guid>
{
    protected WalletOwnerId(Guid id) : base(id) { }

    public static WalletOwnerId NewId() => new(Guid.NewGuid());

    public override WalletOwnerId Copy() => new(Id);
}
