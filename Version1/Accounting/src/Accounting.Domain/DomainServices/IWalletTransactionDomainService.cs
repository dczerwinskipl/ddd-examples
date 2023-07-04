using Accounting.Contracts.DTOs;
using Accounting.Domain.Aggregates;
using Accounting.Domain.Policies;

namespace Accounting.Domain.DomainServices;

public interface IWalletTransactionDomainService
{
    WalletSettlementPeriod DepositMoney(DepositMoneyDTO depositTransaction, WalletOwnership walletOwnership, WalletSettlementPeriod settlement);
    WalletSettlementPeriod WithdrawMoney(WithdrawMoneyDTO withdrawTransaction, DateTimeOffset now, WalletOwnership walletOwnership, WalletType walletType, WalletSettlementPeriod settlement, IWalletDebtPolicyFactory factory);
}
