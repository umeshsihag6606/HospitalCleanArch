using Domain.Comman.Interfaces;
using MediatR;

namespace Domain.Comman;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAndCleanEvents(IEnumerable<BaseEntity> entitesWithEvents)
    {
       foreach (var entity in entitesWithEvents)
        {
            var events=entity.DomainEvent.ToArray();
            entity.ClearDomainInEvent();
            foreach(var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
