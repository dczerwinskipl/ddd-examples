public record InvoicePaymentSettled(Guid InvoiceId, Guid BankTransferId, decimal Amount) : InvoiceDomainEvent(InvoiceId);

