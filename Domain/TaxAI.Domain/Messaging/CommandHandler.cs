using FluentValidation.Results;
using TaxAI.Domain.Interfaces;

namespace TaxAI.Domain.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;
        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }
        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }
        protected async Task<ValidationResult> Commit(IRepositoryUow uow, string message)
        {
            if (!await uow.SaveAsync())
            {
                AddError(message);
            }
            return ValidationResult;
        }
        protected async Task<ValidationResult> CommitBulk(IRepositoryUow uow, string message)
        {
            if (!await uow.BulkSalve())
            {
                AddError(message);
            }
            return ValidationResult;
        }
        protected async Task<ValidationResult> Commit<TEntity>(IRepositoryUow uow, TEntity entity, ValidationResult validationResult) where TEntity : Entity
        {
            validationResult = ValidationResult;
            return await Commit(uow, "There was an error saving data").ConfigureAwait(false);
        }
        protected async Task<ValidationResult> Commit<TEntity>(IRepositoryUow uow, List<TEntity> entities, ValidationResult validationResult = default) where TEntity : Entity
        {
            validationResult = ValidationResult;
            return await CommitBulk(uow, "There was an error saving data").ConfigureAwait(false);
        }
    }
}
