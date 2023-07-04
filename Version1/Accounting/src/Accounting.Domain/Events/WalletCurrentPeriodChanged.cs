using Accounting.Contracts.ValueObjects;
using Accounting.Domain.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletCurrentPeriodChanged(WalletId WalletId, SettlementPeriod CurrentPeriod) : WalletPrivateDomainEvent(WalletId);
