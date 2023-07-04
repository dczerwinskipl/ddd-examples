namespace Accounting.DTOs;

public class TransactionDTO
{
    public Guid TransactionId { get; set; }
    public DateTimeOffset Date { get; set; }
    public decimal Amount { get; set; }
}
