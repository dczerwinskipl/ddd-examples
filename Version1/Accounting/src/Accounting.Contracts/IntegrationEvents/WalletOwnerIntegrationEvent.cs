using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Contracts.IntegrationEvents;

public abstract record WalletOwnerIntegrationEvent(WalletOwnerId WalletOwnerId) : IntegrationEvent;
