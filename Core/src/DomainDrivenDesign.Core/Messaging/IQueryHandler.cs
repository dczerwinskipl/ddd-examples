namespace DomainDrivenDesign.Core.Messaging;

public interface IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}

