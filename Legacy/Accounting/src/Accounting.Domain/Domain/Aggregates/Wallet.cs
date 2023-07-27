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

    internal WalletType WalletType { get; set; }

    internal Guid OwnerId { get; set; }
    internal string OwnerFirstName { get; set; }
    internal string? OwnerMiddleName { get; set; }
    internal string OwnerLastName { get; set; }
    internal string OwnerPhoneNumber { get; set; }

    internal decimal Balance { get; set; }
    internal List<WalletTransaction> Transactions { get; } = new List<WalletTransaction>();

    internal bool WalletIsActive { get; set; }
    internal bool OwnerUserIsActive { get; set; }
    internal bool UserHasLegalPoceedings { get; set; }
    internal bool? UserLegalPoceedingsDiscontinued { get; set; }
    internal DateTime? WalletContractExpireDate { get; set; }
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

    internal class WalletTransaction : Entity<Guid>
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
