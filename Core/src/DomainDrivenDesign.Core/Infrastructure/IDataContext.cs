using DomainDrivenDesign.Core.BuildingBlocks;

namespace DomainDrivenDesign.Core.Infrastructure;

public interface IDataContext<TEntity, TKey> where TEntity : Entity<TKey>
{
    TEntity? Get(TKey key);
    Task<TEntity?> GetAsync(TKey key, CancellationToken? cancellationToken = null);
    IQueryable<TEntity> GetAll();
    void Save(TEntity entity);
    Task SaveAsync(TEntity entity, CancellationToken? cancellationToken = null);
}
