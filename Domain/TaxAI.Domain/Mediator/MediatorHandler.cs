using FluentValidation.Results;
using MediatR;
using TaxAI.Domain.Interfaces;
using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Mediator
{
    public abstract class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
        public virtual Task PublishEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
