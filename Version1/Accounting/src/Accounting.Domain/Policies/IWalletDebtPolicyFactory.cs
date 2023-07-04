using Accounting.Domain.ValueObjects;

namespace Accounting.Domain.Policies;

public interface IWalletDebtPolicyFactory
{
    IWalletDebtPolicy GetVIPWalletPolicy();
    IWalletDebtPolicy GetDefaultPolicy();
    IWalletDebtPolicy CreatePolicy(WalletTypeSnapshot value);
}