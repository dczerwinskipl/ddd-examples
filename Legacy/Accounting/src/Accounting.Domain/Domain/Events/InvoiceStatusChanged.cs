using Accounting.Domain.Aggregates;

public record InvoiceStatusChanged(Guid InvoiceId, InvoiceStatus Status) : InvoiceDomainEvent(InvoiceId);

