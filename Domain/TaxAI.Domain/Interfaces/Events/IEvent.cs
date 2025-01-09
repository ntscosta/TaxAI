using MediatR;
using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces.Events
{
    public interface IEvent<T> : INotification
        where T : Event
    {
    }
}
