using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Aggregates;

namespace Accounting.Domain.Repositories;

public interface IWalletOwnershipRepository : IWalletAggregateRepository<WalletOwnership, WalletOwnershipId>
{
}
