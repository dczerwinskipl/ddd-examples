using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Aggregates;


public class BankTransferParty : Entity<Guid>
{
    public string? IBAN { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}

public enum BankTransferType
{
    Incoming,
    Outcoming
}

public enum BankTransferStatus
{
    New,
    Sent,
    Confirmed,
    Rejected,
    Deleted,

    // from mbank
    MBankWaitingForApprove = 101,
    MBankSuspended = 102,

    // from santander
    SantanderInQueue = 201,

    // ... and so on
}

public class BankTransfer : AggregateRoot<Guid>
{
    public Guid AccountId { get; set; }
    private BankTransferType Type { get; set; }
    private BankTransferParty Sender { get; set; }
    private BankTransferParty Receiver { get; set; }
    private decimal Amount { get; set; }
    private string Description { get; set; }
    private BankTransferStatus Status { get; set; }
    private Guid? InvoiceId { get; set; }

    public BankTransfer(Guid bankTransferId, Guid accountId, BankTransferType type, BankTransferParty sender, BankTransferParty receiver, decimal amount, string description, Guid? invoiceId) : base(bankTransferId)
    {
        AccountId = accountId;
        Type = type;
        Sender = sender;
        Receiver = receiver;
        Amount = amount;
        Description = description;
        InvoiceId = invoiceId;
        Status = BankTransferStatus.New;
    }

    public void SetStatus(BankTransferStatus status)
    {
        if (Status == status)
            return;

        if (!CanChange(status))
            throw new DomainException();

        Status = status;

        Publish(new BankTransferStatusChanged(Id, status));
    }

    public void SetInvoice(Guid invoiceId)
    {
        if (InvoiceId == invoiceId)
            return;

        if (InvoiceId is not null)
            throw new DomainException();

        InvoiceId = invoiceId;

        Publish(new BankTransferInvoiceSet(Id, invoiceId));
    }

    // and so on
    private static readonly List<(BankTransferStatus From, BankTransferStatus To)> _allowedTransitions = new()
    {
        (BankTransferStatus.New, BankTransferStatus.Sent),
        (BankTransferStatus.Sent, BankTransferStatus.Confirmed),
        (BankTransferStatus.Sent, BankTransferStatus.Rejected),
        (BankTransferStatus.New, BankTransferStatus.Deleted),
        // and so on
    };

    private bool CanChange(BankTransferStatus status) => 
        _allowedTransitions.Any(t => t.From == Status && t.To == status)
        // for custom bank statuses
        || (int)status > 100
        || (int)Status > 100;
}
