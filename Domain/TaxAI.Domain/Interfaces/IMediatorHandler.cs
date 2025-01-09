using FluentValidation.Results;
using TaxAI.Domain.Messaging;

namespace TaxAI.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}