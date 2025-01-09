using FluentValidation.Results;
using MediatR;

namespace TaxAI.Domain.Interfaces.Commands
{
    public interface ICommandRemove : ICommand, IAggregateRoot
    {
    }
}
