using TaxAI.Domain.Interfaces.Events;

namespace TaxAI.Domain.Messaging
{
    public abstract class EventCreate : Event, IEventCreate<EventCreate>
    {
    }
}
