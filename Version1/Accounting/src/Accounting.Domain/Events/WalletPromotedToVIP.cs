using Accounting.Contracts.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletPromotedToVIP(WalletId WalletId) : WalletPrivateDomainEvent(WalletId);
