using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.Events;

public record WalletTransactionSettled(WalletId WalletId, TransactionId TransactionId, DateTimeOffset Date, Money Amount) : WalletPrivateDomainEvent(WalletId);
