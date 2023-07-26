namespace Accounting.DTOs;

public record InvoiceLineDTO(decimal Amount, decimal Tax, string Description);
