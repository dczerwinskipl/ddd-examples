using System.Diagnostics.CodeAnalysis;
using Accounting.DTOs;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Aggregates;

public enum InvoiceStatus
{
    Paid, 
    Unpaid
}

public class Invoice : AggregateRoot<Guid>
{
    private List<InvoiceLine> Lines { get; set; } = new();

    private decimal TotalAmount => TotalNetAmount + TotalTax;
    private decimal TotalNetAmount => Lines.Sum(i => i.NetAmount);
    private decimal TotalTax => Lines.Sum(i => i.Tax);

    private List<InvoicePayment> InvoicePayments { get; set; } = new List<InvoicePayment>();
    private InvoiceStatus Status { get; set; }

    protected Invoice(Guid id, IEnumerable<InvoiceLineDTO> lines) : base(id)
    {
        Lines = lines.Select(InvoiceLine.Create).ToList();
        RecalulateStatus();
    }

    public void AddPayment(Guid bankTransferId, decimal amount)
    {
        InvoicePayments.Add(new InvoicePayment(bankTransferId, amount));
        Publish(new InvoicePaymentSettled(Id, bankTransferId, amount));
        RecalulateStatus();
    }

    private void RecalulateStatus()
    {
        var oldStatus = Status;
        Status = TotalAmount <= InvoicePayments.Sum(p => p.Amount) ? InvoiceStatus.Paid : InvoiceStatus.Unpaid;
        if (oldStatus != Status)
            Publish(new InvoiceStatusChanged(Id, Status));
    }

    public (decimal TotalAmount, decimal TotalNetAmount, decimal TotalTax) GetSnapshot() => (TotalAmount, TotalNetAmount, TotalTax);

    public class InvoiceLine : Entity<Guid>
    {
        internal decimal NetAmount { get; set; }
        internal decimal Tax { get; set; }
        internal string Description { get; set; }

        public InvoiceLine() { }
        [SetsRequiredMembers]
        public InvoiceLine(Guid id, decimal amount, decimal tax, string description) : base(id)
        {
            NetAmount = amount;
            Tax = tax;
            Description = description;
        }

        public static InvoiceLine Create(InvoiceLineDTO invoiceLineDTO) => new(Guid.NewGuid(), invoiceLineDTO.Amount, invoiceLineDTO.Tax, invoiceLineDTO.Description);
    }

    public class InvoicePayment : Entity<Guid>
    {
        internal decimal Amount { get; set; }

        [SetsRequiredMembers]
        public InvoicePayment(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}
