using DomainDrivenDesign.Core.BuildingBlocks;

namespace DomainDrivenDesign.Core.Infrastructure;

public interface IRepository<TAggregate, TKey> where TAggregate : AggregateRoot<TKey>
{
    TAggregate? Get(TKey key);
    Task<TAggregate?> GetAsync(TKey key, CancellationToken cancellationToken);
    void Save(TAggregate aggregate);
    Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken);
}
