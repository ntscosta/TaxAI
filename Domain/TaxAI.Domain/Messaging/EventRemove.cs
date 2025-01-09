using TaxAI.Domain.Interfaces.Events;

namespace TaxAI.Domain.Messaging
{
    public abstract class EventRemove : Event, IEventRemove<EventRemove>
    {
    }
}
