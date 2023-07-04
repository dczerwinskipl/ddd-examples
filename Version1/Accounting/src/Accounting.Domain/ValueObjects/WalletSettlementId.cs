using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletSettlementId : Identifier<WalletSettlementId, Guid>
{
    protected WalletSettlementId(Guid id) : base(id) { }

    public static WalletSettlementId NewId() => new(Guid.NewGuid());

    public override WalletSettlementId Copy() => new(Id);
}
