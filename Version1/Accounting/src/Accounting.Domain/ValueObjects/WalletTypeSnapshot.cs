using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.ValueObjects;

public class WalletTypeSnapshot : ValueObject
{
    public WalletId WalletId { get; }
    public bool IsVIP { get; set; }

    public WalletTypeSnapshot(WalletId walletId, bool isVIP)
    {
        WalletId = walletId;
        IsVIP = isVIP;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return WalletId;
        yield return IsVIP;
    }
}


public class WalletSettlementPeriodSnapshot : ValueObject
{
    public WalletId WalletId { get; set; }
    public SettlementPeriod SettlementPeriod { get; set; }
    public Money OpeningBalance { get; set; }
    public Money ClosingBalance { get; set; }

    public WalletSettlementPeriodSnapshot(WalletId walletId, SettlementPeriod settlementPeriod, Money openingBalance, Money closingBalance)
    {
        WalletId = walletId;
        SettlementPeriod = settlementPeriod;
        OpeningBalance = openingBalance;
        ClosingBalance = closingBalance;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return WalletId;
        yield return SettlementPeriod;
        yield return OpeningBalance;
        yield return ClosingBalance;
    }
}