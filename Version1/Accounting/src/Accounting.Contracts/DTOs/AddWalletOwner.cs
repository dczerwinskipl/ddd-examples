using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.DTOs;

public record AddWalletOwner(WalletOwnerId? WalletOwnerId, PersonalData PersonalData);
