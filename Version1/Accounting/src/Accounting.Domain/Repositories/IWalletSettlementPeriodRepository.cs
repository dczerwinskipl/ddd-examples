using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Aggregates;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.Infrastructure;

namespace Accounting.Domain.Repositories;

public interface IWalletSettlementPeriodRepository : IRepository<WalletSettlementPeriod, WalletSettlementPeriodId>
{
    WalletSettlementPeriod GetByWalletIdAndPeriod(WalletId walletId, SettlementPeriod settlementPeriod);
    Task<WalletSettlementPeriod> GetByWalletIdAndPeriodAsync(WalletId walletId, SettlementPeriod settlementPeriod, CancellationToken cancellationToken);
}
