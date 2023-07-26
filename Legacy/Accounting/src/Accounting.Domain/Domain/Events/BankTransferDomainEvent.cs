using DomainDrivenDesign.Core.Messaging;

public abstract record BankTransferDomainEvent(Guid BankTransferId) : DomainEvent;

