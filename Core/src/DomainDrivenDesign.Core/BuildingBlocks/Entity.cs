using System.Diagnostics.CodeAnalysis;

namespace DomainDrivenDesign.Core.BuildingBlocks;

public abstract class Entity<TKey>
{
    public required TKey Id { get; init; }

    protected Entity() { }

    [SetsRequiredMembers]
    protected Entity(TKey id)
    {
        Id = id;
    }
}
