using Accounting.Contracts.ValueObjects;
using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.Entities;

public class Wallet : Entity<WalletId>
{
    //CRUD properties
    public required string Name { get; set; }
    public string? Description { get; set; }
}
