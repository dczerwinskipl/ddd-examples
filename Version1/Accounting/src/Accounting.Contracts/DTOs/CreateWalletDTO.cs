using Accounting.Contracts.ValueObjects;

namespace Accounting.Contracts.DTOs;

public record CreateWalletDTO(WalletId WalletId, WalletOwnerId OwnerId, string WalletName, string? WalletDescription);
