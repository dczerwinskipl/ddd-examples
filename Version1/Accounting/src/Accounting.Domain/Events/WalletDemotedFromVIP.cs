using Accounting.Contracts.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletDemotedFromVIP(WalletId WalletId) : WalletPrivateDomainEvent(WalletId);