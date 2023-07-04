using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.Policies;

public interface IWalletDebtPolicy
{
    Money GetAvailableDebt();
}
