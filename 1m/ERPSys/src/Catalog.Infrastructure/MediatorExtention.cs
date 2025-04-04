namespace Catalog.Infrastructure;

static class MediatorExtention
{
    public static async Task DispatchDomainEventAsinc(this IMediator mediator, CatalogsContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
        
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
        
        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
    
}