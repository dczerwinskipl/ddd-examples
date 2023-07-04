using Accounting.Domain.Aggregates;
using Accounting.DTOs;

namespace Accounting.Domain.Events;

public record WalletCreated(Guid WalletId, string Name, string? Description, PersonDTO Owner, WalletType NewType) : WalletDomainEvent(WalletId);
