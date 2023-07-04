using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Contracts.IntegrationEvents;

public record WalletCreated(WalletId WalletId, string Name, string? Description, WalletOwnerId OwnerId) : IntegrationEvent;