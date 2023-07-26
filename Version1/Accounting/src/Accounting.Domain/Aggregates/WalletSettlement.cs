using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Events;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Aggregates;

public class WalletSettlement : AggregateRoot<WalletSettlementId>
{
    public WalletId WalletId { get; internal set; }
    internal SettlementPeriod CurrentPeriod { get; set; }

    internal WalletSettlement() { }
    protected WalletSettlement(WalletId walletId, DateTimeOffset date) : base(WalletSettlementId.NewId())
    {
        WalletId = walletId;
        CurrentPeriod = date;
        Publish(new WalletCurrentPeriodChanged(WalletId, CurrentPeriod));
    }

    public void CloseCurrentPeriod()
    {
        var nextPeriod = CurrentPeriod.GetNextPeriod();
        CurrentPeriod = nextPeriod;
        Publish(new WalletCurrentPeriodChanged(WalletId, CurrentPeriod));
    }
    public SettlementPeriod GetCurrentPeriod() => CurrentPeriod;

    public static WalletSettlement CreateFromDate(WalletId walletId, DateTimeOffset date) => new(walletId, date);

}


