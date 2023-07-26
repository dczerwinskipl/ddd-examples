using System.Transactions;
using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Aggregates;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.ValueObjects;
using FluentAssertions.Execution;
using static Accounting.Domain.Aggregates.WalletSettlementPeriod;

namespace Accounting.Domain.Tests.Builders;

public class WalletSettlementPeriodBuilder
{
    private Money _openingBalance = 0;
    private Money _debit = 0;
    private Money _credit = 0;
    private SettlementPeriod _settlementPeriod = DateTimeOffset.UtcNow;
    private List<WalletTransactionBuilder> _transactions = new();

    private WalletSettlementPeriodBuilder() { }

    public static WalletSettlementPeriodBuilder Given => new();

    public WalletSettlementPeriodBuilder WithOpeningBalance(Money amount) { _openingBalance = amount; return this; }
    public WalletSettlementPeriodBuilder WithDebit(Money amount) { _debit = amount; return this; }
    public WalletSettlementPeriodBuilder WithCredit(Money amount) { _credit = amount; return this; }
    public WalletSettlementPeriodBuilder ForPeriod(SettlementPeriod period) { _settlementPeriod = period; return this; }
    public WalletSettlementPeriodBuilder WithSettledTransaction(TransactionId? transactionId, Action<WalletTransactionBuilder>? configure = null)
    {
        var builder = WalletTransactionBuilder.Given;
        if (transactionId is not null)
            builder.WithId(transactionId);

        configure?.Invoke(builder);
        _transactions.Add(builder);
        return this;
    }
    public WalletSettlementPeriodBuilder WithSettledTransaction(Action<WalletTransactionBuilder>? configure = null) => WithSettledTransaction(null, configure);

    public WalletSettlementPeriod Build() => new WalletSettlementPeriod() { 
        SettlementPeriod = _settlementPeriod,
        OpeningBalance = _openingBalance, 
        Debit = _debit,
        Credit = _credit, 
        Transactions = _transactions.Select(t => t.Build()).ToList()
        
    };
}
