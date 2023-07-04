using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.Events;

public abstract record WalletDomainEvent(Guid WalletId) : DomainEvent;