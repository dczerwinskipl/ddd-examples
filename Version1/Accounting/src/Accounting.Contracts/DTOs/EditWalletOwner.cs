using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.DTOs;

public record EditWalletOwner(WalletOwnerId WalletOwnerId, PersonalData PersonalData);
