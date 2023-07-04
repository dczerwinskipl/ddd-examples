using System.Diagnostics.CodeAnalysis;
using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Events;
using Accounting.Domain.Policies;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.Aggregates;

public class WalletSettlementPeriod : AggregateRoot<WalletSettlementPeriodId>
{
    public WalletId WalletId { get; private set; }
    public SettlementPeriod SettlementPeriod { get; private set; }
    private Money OpeningBalance { get; set; }
    private Money Balance { get; set; }
    private List<WalletTransaction> Transactions { get; } = new List<WalletTransaction>();

    protected WalletSettlementPeriod() { }

    [SetsRequiredMembers]
    protected WalletSettlementPeriod(WalletId walletId, SettlementPeriod settlementPeriod, Money openingBalance) : base(WalletSettlementPeriodId.NewId())
    {
        WalletId = walletId;

        SettlementPeriod = settlementPeriod;
        OpeningBalance = openingBalance;

        Balance = 0.00M;
        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
    }

    public void Deposit(TransactionId transactionId, DateTimeOffset date, Money amount)
    {
        if (date != SettlementPeriod)
            throw new ArgumentException(nameof(date));

        if (amount < 0)
            throw new ArgumentException(nameof(amount));

        if (Transactions.Any(t => t.Id == transactionId))
            return;

        ChangeBalance(amount);
        AddTransaction(WalletTransaction.Create(transactionId), date, amount);
    }

    public void Withdraw(TransactionId transactionId, DateTimeOffset now, Money amount, IWalletDebtPolicy debtPolicy)
    {
        if (now != SettlementPeriod)
            throw new ArgumentException(nameof(now));

        if (amount < 0)
            throw new ArgumentException(nameof(amount));

        if (Balance + debtPolicy.GetAvailableDebt() < amount)
            throw new DomainException();

        if (Transactions.Any(t => t.Id == transactionId))
            return;

        ChangeBalance(-amount);
        AddTransaction(WalletTransaction.Create(transactionId), now, -amount);
    }

    private void AddTransaction(WalletTransaction transaction, DateTimeOffset date, Money amount)
    {
        Transactions.Add(transaction);
        Publish(new WalletTransactionSettled(WalletId, transaction.Id, date, amount));
    }

    private void ChangeBalance(Money money)
    {
        Balance += money;
        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
    }

    public WalletSettlementPeriodSnapshot GetSnapshot() => new(WalletId, SettlementPeriod, OpeningBalance, Balance);

    public static WalletSettlementPeriod CreateNewPeriod(WalletId walletId, SettlementPeriod settlementPeriod, Money openingBalance, IWalletDebtPolicy walletDebtPolicy) => new(walletId, settlementPeriod, openingBalance, walletDebtPolicy);

    private class WalletTransaction : Entity<TransactionId>
    {
        public WalletTransaction()
        {

        }

        [SetsRequiredMembers]
        protected WalletTransaction(TransactionId transactionId) : base(transactionId) { }

        public static WalletTransaction Create(TransactionId transactionId) => new(transactionId);
    }
}


