namespace Accounting.Domain.Events;

public record WalletOwnerPhoneNumberChanged(Guid WalletId, string PhoneNumber) : WalletDomainEvent(WalletId);
