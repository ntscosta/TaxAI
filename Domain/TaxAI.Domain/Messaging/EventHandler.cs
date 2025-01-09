using MediatR;
using TaxAI.Domain.Interfaces;

namespace TaxAI.Domain.Messaging
{
    public abstract class EventHandler :
        INotificationHandler<EventCreate>,
        INotificationHandler<EventRemove>,
        INotificationHandler<EventUpdate>
    {
        public Task Handle(EventCreate notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(EventRemove notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(EventUpdate notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
