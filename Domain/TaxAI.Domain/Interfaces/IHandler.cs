using FluentValidation.Results;
using MediatR;
using TaxAI.Domain.Interfaces.Commands;
using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces
{
    public interface IHandler<TEntity, TEvent, TCommand> :
        IRequestHandler<ICommandCreate, ValidationResult>,
        IRequestHandler<ICommandRemove, ValidationResult>,
        IRequestHandler<ICommandUpdate, ValidationResult>
        where TEntity : Entity
        where TEvent : Event
        where TCommand : Command
    {
    }
}
