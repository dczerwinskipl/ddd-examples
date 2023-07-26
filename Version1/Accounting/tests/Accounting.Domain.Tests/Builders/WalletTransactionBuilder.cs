using Accounting.Contracts.ValueObjects;
using static Accounting.Domain.Aggregates.WalletSettlementPeriod;

namespace Accounting.Domain.Tests.Builders;

public class WalletTransactionBuilder
{
    private TransactionId _transactionId = TransactionId.NewId();
    private WalletTransactionBuilder() { }
    public static WalletTransactionBuilder Given => new();

    public WalletTransactionBuilder WithId(TransactionId transactionId) { _transactionId = transactionId; return this; }

    internal WalletTransaction Build() => new WalletTransaction(_transactionId);
}
