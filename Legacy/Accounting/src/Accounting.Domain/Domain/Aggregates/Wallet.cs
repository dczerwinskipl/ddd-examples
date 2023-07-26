using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using Accounting.Domain.Events;
using Accounting.DTOs;
using DomainDrivenDesign.Core.BuildingBlocks;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.Aggregates;

public class Wallet : AggregateRoot<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    private WalletType WalletType { get; set; }

    private Guid OwnerId { get; set; }
    private string OwnerFirstName { get; set; }
    private string? OwnerMiddleName { get; set; }
    private string OwnerLastName { get; set; }
    private string OwnerPhoneNumber { get; set; }

    private decimal Balance { get; set; }
    private List<WalletTransaction> Transactions { get; } = new List<WalletTransaction>();

    private bool WalletIsActive { get; set; }
    private bool OwnerUserIsActive { get; set; }
    private bool UserHasLegalPoceedings { get; set; }
    private bool? UserLegalPoceedingsDiscontinued { get; set; }
    private DateTime? WalletContractExpireDate { get; set; }
    public bool WalletContractRenounced { get; set; }

    private Wallet(Guid id, string name, string? description, PersonDTO owner, WalletType walletType = WalletType.Standard) : base(id)
    {
        Name = name;
        Description = description;
        Balance = 0;
        WalletType = walletType;
        OwnerId = owner.Id;
        OwnerFirstName = owner.FirstName;
        OwnerMiddleName = owner.MiddleName;
        OwnerLastName = owner.LastName;
        OwnerPhoneNumber = owner.PhoneNumber;
        WalletIsActive = true;
        OwnerUserIsActive = true;
        Publish(new WalletCreated(Id, Name, Description, owner.Copy(), walletType));
    }

    public static Wallet Create(Guid id, string name, string? description, PersonDTO owner) => new(id, name, description, owner);

    public void Deposit(Guid ownerId, TransactionDTO transaction)
    {
        if (!IsActive())
            throw new DomainException();

        if (Transactions.Any(t => t.Id == transaction.TransactionId))
            return;

        if (OwnerId != ownerId)
            throw new DomainException();

        Balance += transaction.Amount;
        Publish(new WalletBalanceChanged(Id, Balance));

        Transactions.Add(WalletTransaction.Create(transaction));
        Publish(new WalletTransactionSettled(Id, transaction.TransactionId, transaction.Date, transaction.Amount));
    }

    public void Withdraw(Guid ownerId, TransactionDTO transaction)
    {
        if (!IsActive())
            throw new DomainException();

        if (Transactions.Any(t => t.Id == transaction.TransactionId))
            return;

        if (OwnerId != ownerId)
            throw new DomainException();

        var debtLimit = WalletType == WalletType.VIP ? 1000 : 0;
        if (Balance + debtLimit < transaction.Amount)
            throw new DomainException();

        Balance -= transaction.Amount;
        Publish(new WalletBalanceChanged(Id, Balance));

        Transactions.Add(WalletTransaction.Create(transaction));
        Publish(new WalletTransactionSettled(Id, transaction.TransactionId, transaction.Date, -transaction.Amount));
    }

    public void ChangeOwner(PersonDTO owner)
    {
        if (!IsActive())
            throw new DomainException();

        OwnerId = owner.Id;
        OwnerFirstName = owner.FirstName;
        OwnerMiddleName = owner.MiddleName;
        OwnerLastName = owner.LastName;
        OwnerPhoneNumber = owner.PhoneNumber;
        Publish(new WalletOwnerChanged(Id, owner.Copy()));
    }

    public void ChangePhoneNumber(string phoneNumber)
    {
        if (!IsActive())
            throw new DomainException();

        OwnerPhoneNumber = phoneNumber;
        Publish(new WalletOwnerPhoneNumberChanged(Id, OwnerPhoneNumber));
    }

    public void ChangeType(WalletType type)
    {
        if (!IsActive())
            throw new DomainException();

        if (WalletType == type)
            return;

        WalletType = type;
        Publish(new WalletTypeChanged(Id, WalletType));
    }

    private bool IsActive()
    {
        return
            WalletIsActive
            && OwnerUserIsActive
            && (!UserHasLegalPoceedings || (UserLegalPoceedingsDiscontinued ?? false))
            && (WalletContractExpireDate is null || WalletContractExpireDate > DateTime.UtcNow)
            && !WalletContractRenounced;
    } 

    private class WalletTransaction : Entity<Guid>
    {
        public required DateTimeOffset Date { get; init; }
        public required Money Amount { get; init; }
        public WalletTransaction()
        {
        }

        [SetsRequiredMembers]
        protected WalletTransaction(Guid transactionId, DateTimeOffset date, Money amount) : base(transactionId)
        {
            Date = date;
            Amount = amount;
        }


        public static WalletTransaction Create(Guid transactionId, DateTimeOffset date, Money amount) => new(transactionId, date, amount);
        public static WalletTransaction Create(TransactionDTO transactionDTO) => new(transactionDTO.TransactionId, transactionDTO.Date, transactionDTO.Amount);
    }
}

public enum WalletType
{
    Standard,
    VIP
}
