using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.ValueObjects;

public class WalletSettlementPeriodId : Identifier<WalletSettlementPeriodId, Guid>
{
    protected WalletSettlementPeriodId(Guid id) : base(id) { }

    public static WalletSettlementPeriodId NewId() => new(Guid.NewGuid());

    public override WalletSettlementPeriodId Copy() => new(Id);
}
