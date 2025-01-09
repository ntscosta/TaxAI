using FluentValidation.Results;
using MediatR;


namespace TaxAI.Domain.Interfaces.Commands
{
    public interface ICommand : IRequest<ValidationResult>, IBaseRequest
    {
        ValidationResult ValidationResult { get; }
        bool IsValid();
    }
}
