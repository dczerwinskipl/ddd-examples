using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Events;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Aggregates;

public class WalletOwnership : AggregateRoot<WalletOwnershipId>
{
    public WalletId WalletId { get; private set; }
    private WalletOwnerId OwnerId { get; set; }

    protected WalletOwnership() { }
    protected WalletOwnership(WalletId walletId, WalletOwnerId ownerId) : base(WalletOwnershipId.NewId())
    {
        WalletId = walletId;
        OwnerId = ownerId;
    }

    public static WalletOwnership CreateFor(WalletId wallet, WalletOwnerId owner) => new(wallet, owner);

    public void ChangeOwner(WalletOwnerId owner)
    {
        OwnerId = owner;
        Publish(new WalletOwnerChanged(WalletId, OwnerId));
    }

    public bool CanDeposit(WalletOwnerId who) => IsOwner(who);

    public bool CanWithdraw(WalletOwnerId who) => IsOwner(who);

    private bool IsOwner(WalletOwnerId who) => OwnerId == who;
}
