using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.Events;

public abstract record WalletPrivateDomainEvent(WalletId WalletId) : Event;
