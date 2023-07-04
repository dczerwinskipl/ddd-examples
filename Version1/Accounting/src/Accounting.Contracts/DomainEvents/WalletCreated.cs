using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.Messaging;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Contracts.DomainEvents;

public record WalletCreated(WalletId Wallet, ObjectId Owner) : DomainEvent;