using System.Collections.Generic;
using MediatR;

namespace MovieSystem.Domain.SeedWork;
public abstract class Entity
{
    public virtual int Id { get; protected set; }

    private List<INotification>? _domainEvents;
    public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() =>
        _domainEvents?.Clear();
}
