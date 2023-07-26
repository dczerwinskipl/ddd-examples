using System.Diagnostics;
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
    public WalletId WalletId { get; internal set; }
    public SettlementPeriod SettlementPeriod { get; internal set; }
    internal Money OpeningBalance { get; set; }
    internal Money Debit { get; set; }
    internal Money Credit { get; set; }
    internal Money Balance => OpeningBalance + (Debit - Credit);
    internal List<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();

    internal WalletSettlementPeriod() { }

    [SetsRequiredMembers]
    protected WalletSettlementPeriod(WalletId walletId, SettlementPeriod settlementPeriod, Money openingBalance) : base(WalletSettlementPeriodId.NewId())
    {
        WalletId = walletId;

        SettlementPeriod = settlementPeriod;
        OpeningBalance = openingBalance;
        Debit = 0;
        Credit = 0;

        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
    }

    public void ChangeOpeningBalance(Money openingBalance)
    {
        OpeningBalance = openingBalance;
        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
    }

    public void Deposit(TransactionId transactionId, DateTimeOffset date, Money amount)
    {
        if ((SettlementPeriod)date != SettlementPeriod)
            throw new ArgumentException(nameof(date));

        if (amount < 0)
            throw new ArgumentException(nameof(amount));

        if (Transactions.Any(t => t.Id == transactionId))
            return;

        Debit += amount;

        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
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

        Credit += amount;

        Publish(new WalletBalanceChanged(WalletId, SettlementPeriod, Balance));
        AddTransaction(WalletTransaction.Create(transactionId), now, -amount);
    }

    internal void AddTransaction(WalletTransaction transaction, DateTimeOffset date, Money amount)
    {
        Transactions.Add(transaction);
        Publish(new WalletTransactionSettled(WalletId, transaction.Id, date, amount));
    }

    public WalletSettlementPeriodSnapshot GetSnapshot() => new(WalletId, SettlementPeriod, OpeningBalance, Balance);

    public static WalletSettlementPeriod CreateNewPeriod(WalletId walletId, SettlementPeriod settlementPeriod, Money openingBalance) => new(walletId, settlementPeriod, openingBalance);

    internal class WalletTransaction : Entity<TransactionId>
    {
        public WalletTransaction()
        {

        }

        [SetsRequiredMembers]
        internal WalletTransaction(TransactionId transactionId) : base(transactionId) { }

        public static WalletTransaction Create(TransactionId transactionId) => new(transactionId);
    }
}

