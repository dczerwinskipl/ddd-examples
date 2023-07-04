using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.IntegrationEvents;

public record WalletOwnerRemoved(WalletOwnerId WalletOwnerId) : WalletOwnerIntegrationEvent(WalletOwnerId);
