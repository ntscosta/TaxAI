using AutoMapper;
using FluentValidation.Results;
using TaxAI.Domain.Interfaces;
using TaxAI.Domain.Interfaces.Commands;

namespace TaxAI.Domain.Messaging
{
    public abstract class Handler<TEntity, TEvent, TCommand> : CommandHandler, IHandler<TEntity, TEvent, TCommand>
        where TEntity : Entity
        where TEvent : Event
        where TCommand : Command
    {
        private readonly IRepository<Entity, Guid> Repository;
        private readonly IMapper Mapper;

        protected Handler(IRepository<Entity, Guid> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<ValidationResult> Handle(ICommandCreate message, CancellationToken cancellationToken)
        {
            message.IsValid();
            var model = Mapper.Map<TEntity>(message);
            await Repository.Add(model);
            var @event = Mapper.Map<TEvent>(model);
            model.AddDomainEvent(@event);
            return await Commit(Repository.UnitOfWork, model, message.ValidationResult);
        }

        public async Task<ValidationResult> Handle(ICommandRemove message, CancellationToken cancellationToken)
        {
            var model = await Repository.GetById(message.Id);
            if (model is null)
            {
                AddError($"The {nameof(model)} doesn't exists."); return ValidationResult;
            }
            Event @event = Activator.CreateInstance<TEvent>();
            @event.Id = message.Id;
            model.AddDomainEvent(@event);
            Repository.Remove(model);
            return await Commit(Repository.UnitOfWork, model, message.ValidationResult);
        }

        public async Task<ValidationResult> Handle(ICommandUpdate message, CancellationToken cancellationToken)
        {
            var model = Mapper.Map<TEntity>(message);
            var existtingModel = await Repository.GetById(message.Id);
            if (existtingModel != null)
            {
                if (existtingModel.Equals(model))
                {
                    AddError($"The {nameof(model)} has already been taken."); return ValidationResult;
                }
            }
            var modelEvent = Mapper.Map<TEvent>(existtingModel);
            model.AddDomainEvent(modelEvent);
            Repository.Update(model);
            return await Commit(Repository.UnitOfWork, model, message.ValidationResult).ConfigureAwait(false);
        }
    }
}
