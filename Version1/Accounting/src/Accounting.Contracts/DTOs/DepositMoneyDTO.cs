using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.DTOs;

public record DepositMoneyDTO(WalletId WalletId, WalletOwnerId Who, TransactionId TransactionId, DateTime Date, Money Amount);