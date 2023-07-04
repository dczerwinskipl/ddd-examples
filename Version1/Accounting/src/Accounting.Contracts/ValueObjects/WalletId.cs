using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletId : Identifier<WalletId, Guid>
{
    protected WalletId(Guid id) : base(id) { }

    public static WalletId NewId() => new(Guid.NewGuid());

    public override WalletId Copy() => new(Id);
}
