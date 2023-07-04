namespace Accounting.Domain.Events;

public record WalletBalanceChanged(Guid WalletId, decimal NewBalance) : WalletDomainEvent(WalletId);
