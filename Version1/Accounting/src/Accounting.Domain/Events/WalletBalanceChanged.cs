using Accounting.Contracts.ValueObjects;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletBalanceChanged(WalletId WalletId, SettlementPeriod Period, Money Balance) : WalletPrivateDomainEvent(WalletId);
