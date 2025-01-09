using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces.Events
{
    public interface IEventRemove<T> : IEvent<T>
        where T : Event
    {
    }
}
