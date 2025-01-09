using TaxAI.Domain.Interfaces.Events;

namespace TaxAI.Domain.Messaging
{
    public abstract class EventUpdate : Event, IEventUpdate<EventUpdate>
    {
    }
}
