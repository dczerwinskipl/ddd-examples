using Accounting.Contracts.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletOwnerChanged(WalletId WalletId, WalletOwnerId OwnerId) : WalletPrivateDomainEvent(WalletId);
