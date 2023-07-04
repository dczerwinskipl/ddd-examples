namespace DomainDrivenDesign.Core.Messaging;

public abstract record Event()
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime At { get; } = DateTime.UtcNow;
}
