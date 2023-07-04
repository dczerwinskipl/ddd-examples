namespace DomainDrivenDesign.Core.Messaging;

public interface IEventHandler<TEvent> where TEvent : Event
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}

