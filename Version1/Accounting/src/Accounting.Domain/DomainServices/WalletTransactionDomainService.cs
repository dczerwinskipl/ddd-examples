using Accounting.Contracts.DTOs;
using Accounting.Domain.Aggregates;
using Accounting.Domain.Policies;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.DomainServices;

public class WalletTransactionDomainService : IWalletTransactionDomainService
{
    public WalletSettlementPeriod DepositMoney(DepositMoneyDTO depositTransaction, WalletOwnership walletOwnership, WalletSettlementPeriod settlement)
    {
        if (!walletOwnership.CanDeposit(depositTransaction.Who))
            throw new DomainException();

        settlement.Deposit(depositTransaction.TransactionId, depositTransaction.Date, depositTransaction.Amount);

        return settlement;
    }

    public WalletSettlementPeriod WithdrawMoney(WithdrawMoneyDTO withdrawTransaction, DateTimeOffset now, WalletOwnership walletOwnership, WalletType walletType, WalletSettlementPeriod settlement, IWalletDebtPolicyFactory factory)
    {
        if (!walletOwnership.CanWithdraw(withdrawTransaction.Who))
            throw new DomainException();

        var debtPolicy = factory.CreatePolicy(walletType.GetSnapshot());
        settlement.Withdraw(withdrawTransaction.TransactionId, now, withdrawTransaction.Amount, debtPolicy);

        return settlement;
    }
}
