using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.IntegrationEvents;

public record WalletOwnerPersonalDataChanged(WalletOwnerId WalletOwnerId, PersonalData PersonalDataDTO) : WalletOwnerIntegrationEvent(WalletOwnerId);