using Accounting.DTOs;

namespace Accounting.Domain.Events;

public record WalletOwnerChanged(Guid WalletId, PersonDTO Owner) : WalletDomainEvent(WalletId);