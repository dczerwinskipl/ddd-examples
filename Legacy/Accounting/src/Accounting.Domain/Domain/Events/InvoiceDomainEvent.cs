using DomainDrivenDesign.Core.Messaging;

public abstract record InvoiceDomainEvent(Guid InvoiceId) : DomainEvent;

