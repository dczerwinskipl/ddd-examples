namespace Accounting.Domain.Events;

public record WalletTransactionSettled(Guid WalletId, Guid TransactionId, DateTimeOffset Date, decimal Amount) : WalletDomainEvent(WalletId);
