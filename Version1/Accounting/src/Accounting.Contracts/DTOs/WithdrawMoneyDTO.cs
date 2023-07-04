using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.DTOs;

public record WithdrawMoneyDTO(WalletId WalletId, WalletOwnerId Who, TransactionId TransactionId, Money Amount);
