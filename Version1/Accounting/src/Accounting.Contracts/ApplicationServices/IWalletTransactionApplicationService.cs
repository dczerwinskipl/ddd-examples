using Accounting.Contracts.DTOs;

namespace Accounting.Domain.Services
{
    public interface IWalletTransactionApplicationService
    {
        void DepositMoney(DepositMoneyDTO depositMoneyDTO);
        void WithdrawMoney(WithdrawMoneyDTO withdrawMoneyDTO);
    }
}