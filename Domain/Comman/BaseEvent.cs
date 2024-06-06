using MediatR;

namespace Domain.Comman;

public class BaseEvent:INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
