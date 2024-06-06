using Domain.Comman.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Comman;

public abstract class BaseEntity : IEntity
{
    private readonly List<BaseEvent> _domainEvent = new();
    public int Id { get ; set; }
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvent=> _domainEvent.AsReadOnly();
    public void AddDomainEvent(BaseEvent domainEvent)=>_domainEvent.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent)=>_domainEvent?.Remove(domainEvent);
    public void ClearDomainInEvent()=>_domainEvent.Clear();
}
