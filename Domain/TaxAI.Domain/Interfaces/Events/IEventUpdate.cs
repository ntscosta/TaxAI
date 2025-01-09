using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces.Events
{
    public interface IEventUpdate<T> : IEvent<T>
        where T : Event
    {
    }
}
