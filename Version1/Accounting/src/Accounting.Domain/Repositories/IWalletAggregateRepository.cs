using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;
using DomainDrivenDesign.Core.Infrastructure;

namespace Accounting.Domain.Repositories;

public interface IWalletAggregateRepository<TAggergate, TKey> : IRepository<TAggergate, TKey> where TAggergate : AggregateRoot<TKey>
{
    TAggergate GetByWalletId(WalletId walletId);
    Task<TAggergate> GetByWalletIdAsync(WalletId walletId, CancellationToken cancellationToken);
}
