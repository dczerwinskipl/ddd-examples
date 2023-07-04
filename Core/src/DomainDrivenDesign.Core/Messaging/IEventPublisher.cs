namespace DomainDrivenDesign.Core.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
}
