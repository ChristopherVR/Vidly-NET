using MediatR;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Infrastructure.Extensions;
public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, MovieContext ctx)
    {
        IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity>>? domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents?.Any() == true);

        List<INotification>? domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
