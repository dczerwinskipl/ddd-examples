using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletTypeId : Identifier<WalletTypeId, Guid>
{
    protected WalletTypeId(Guid id) : base(id) { }

    public static WalletTypeId NewId() => new(Guid.NewGuid());

    public override WalletTypeId Copy() => new(Id);
}
