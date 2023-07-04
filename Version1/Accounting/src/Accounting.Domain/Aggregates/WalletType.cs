using Accounting.Contracts.ValueObjects;
using Accounting.Domain.Events;
using Accounting.Domain.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Aggregates;

public class WalletType : AggregateRoot<WalletTypeId>
{
    public WalletId WalletId { get; private set; }
    private bool IsVIP { get; set; } = false;

    protected WalletType() { }
    protected WalletType(WalletId walletId, bool isVIP) : base(WalletTypeId.NewId())
    {
        WalletId = walletId;
        if (isVIP)
            PromoteToVIP();
    }

    public void PromoteToVIP()
    {
        if (IsVIP)
            return;

        IsVIP = true;
        Publish(new WalletPromotedToVIP(WalletId));
    }

    public void DemoteFromVIP()
    {
        if (!IsVIP)
            return;

        IsVIP = false;
        Publish(new WalletDemotedFromVIP(WalletId));
    }

    public static WalletType CreateVipWallet(WalletId wallet) => new(wallet, true);
    public static WalletType CreateStandardWallet(WalletId wallet) => new(wallet, false);

    public WalletTypeSnapshot GetSnapshot() => new(WalletId, IsVIP);
}
