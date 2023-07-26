using Accounting.Domain.Aggregates;
using DomainDrivenDesign.Core.ValueObjects;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Accounting.Domain.Tests.Assertions;

public static class WalletSettlementPeriodAssertionsExtension
{
    public static WalletSettlementPeriodAssertions Should(this WalletSettlementPeriod walletSettlementPeriod) => new WalletSettlementPeriodAssertions(walletSettlementPeriod);
}

public class WalletSettlementPeriodAssertions : ObjectAssertions<WalletSettlementPeriod, WalletSettlementPeriodAssertions>
{
    public WalletSettlementPeriodAssertions(WalletSettlementPeriod value) : base(value)
    {
    }

    public void HaveBalance(Money openingBalance)
    {
        Subject.Balance.Should().BeEquivalentTo(openingBalance);
    }
}
