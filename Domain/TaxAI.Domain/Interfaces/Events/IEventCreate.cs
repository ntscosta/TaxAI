using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces.Events
{
    public interface IEventCreate<T> : IEvent<T>
        where T : Event
    {
    }
}
