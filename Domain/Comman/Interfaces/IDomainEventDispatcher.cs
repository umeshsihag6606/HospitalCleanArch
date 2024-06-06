namespace Domain.Comman.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndCleanEvents(IEnumerable<BaseEntity> entitesWithEvents);
}
