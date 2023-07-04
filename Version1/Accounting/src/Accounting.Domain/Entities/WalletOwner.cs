using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Entities;

public class WalletOwner : Entity<WalletOwnerId>
{
    public required PersonalData PersonalData { get; set; }
}
