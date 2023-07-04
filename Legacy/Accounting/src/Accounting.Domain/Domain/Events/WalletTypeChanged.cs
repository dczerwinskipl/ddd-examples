using Accounting.Domain.Aggregates;

namespace Accounting.Domain.Events;

public record WalletTypeChanged(Guid WalletId, WalletType NewType) : WalletDomainEvent(WalletId);
