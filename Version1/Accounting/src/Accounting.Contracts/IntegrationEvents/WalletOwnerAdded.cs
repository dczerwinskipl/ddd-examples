using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.IntegrationEvents;
public record WalletOwnerAdded(WalletOwnerId WalletOwnerId, PersonalData PersonalDataDTO) : WalletOwnerIntegrationEvent(WalletOwnerId);
