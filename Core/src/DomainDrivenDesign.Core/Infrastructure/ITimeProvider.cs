namespace DomainDrivenDesign.Core.Infrastructure;

public interface ITimeProvider
{
    DateTime GetNow();
    DateTimeOffset GetUtcNow();
}
