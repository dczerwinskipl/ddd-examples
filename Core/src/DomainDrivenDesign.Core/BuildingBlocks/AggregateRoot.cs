using System.Collections.ObjectModel;
using DomainDrivenDesign.Core.Messaging;

namespace DomainDrivenDesign.Core.BuildingBlocks;

public abstract class AggregateRoot<TKey>
{
    public TKey? Id { get; protected set; }

    protected AggregateRoot() { }
    protected AggregateRoot(TKey id)
    {
        Id = id;
    }

    private readonly IList<Event> _events = new List<Event>();
    protected void Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        _events.Add(@event);
    }
    public IReadOnlyCollection<Event> GetEvents() => new ReadOnlyCollection<Event>(_events);
    public IReadOnlyCollection<Event> FlushEvents()
    {
        var events = GetEvents();
        _events.Clear();
        return events;
    }
}