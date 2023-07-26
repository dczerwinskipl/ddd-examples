using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Aggregates;
using Accounting.Domain.Tests.Assertions;
using Accounting.Domain.Tests.Builders;
using DomainDrivenDesign.Core.ValueObjects;
using FluentAssertions;

namespace Accounting.Domain.Tests.Aggregates;

public class WalletSettlementPeriodTests
{
    public static IEnumerable<object[]> OpeningBalances => new List<object[]> {
        new object[] { 0.0M },
        new object[] { -1234.5M },
        new object[] { +1234.5M }
    };

    [Theory]
    [MemberData(nameof(OpeningBalances))]
    public void WhenCreateWithOpeningBalance_ShouldHaveSameBalance(decimal openingBalance)
    {
        // act
        var walletSettlementPeriod = CreateNewPeriod(openingBalance);

        // assert
        walletSettlementPeriod
            .Should()
            .HaveBalance(openingBalance);
    }

    [Fact]
    public void Deposit_WhenTransactionNotSettled_ShouldSettle()
    {
        // arrange
        var openingBalance = Money.Zero;
        var transactionAmount = Money.FromDecimal(200);
        var walletSettlementPeriod = WalletSettlementPeriodBuilder.Given
            .WithOpeningBalance(openingBalance)
            .Build();

        // act
        walletSettlementPeriod.Deposit(TransactionId.NewId(), DateTimeOffset.UtcNow, transactionAmount);

        // assert
        walletSettlementPeriod
            .Should()
            .HaveBalance(transactionAmount);
    }

    [Fact]
    public void Deposit_WhenTransactionAlreadySettled_ShouldIgnore()
    {
        // arrange
        var transactionId = TransactionId.NewId();
        var openingBalance = Money.Zero;
        var walletSettlementPeriod = WalletSettlementPeriodBuilder.Given
            .WithOpeningBalance(openingBalance)
            .WithSettledTransaction(transactionId)
            .Build();

        // act
        walletSettlementPeriod.Deposit(transactionId, DateTimeOffset.UtcNow, 200);

        // assert
        walletSettlementPeriod
            .Should()
            .HaveBalance(openingBalance);
    }


    private static WalletSettlementPeriod CreateNewPeriod(Money openingBalance) => WalletSettlementPeriod.CreateNewPeriod(WalletId.NewId(), DateTime.Now, openingBalance);
}
