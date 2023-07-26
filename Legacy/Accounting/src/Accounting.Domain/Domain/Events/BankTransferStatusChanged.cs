using Accounting.Domain.Aggregates;

public record BankTransferStatusChanged(Guid BankTransferId, BankTransferStatus NewStatus) : BankTransferDomainEvent(BankTransferId);
