namespace Accounting.DTOs;

public record TransactionDTO(Guid TransactionId, DateTimeOffset Date, decimal Amount);