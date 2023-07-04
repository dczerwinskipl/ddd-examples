using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Entities;
using DomainDrivenDesign.Core.Infrastructure;

namespace Accounting.Domain.DataContext;

public interface IWalletDataContext : IDataContext<Wallet, WalletId>
{

}
