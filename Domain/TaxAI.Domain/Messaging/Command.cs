﻿using FluentValidation.Results;
using MediatR;


namespace TaxAI.Domain.Messaging
{
    public abstract class Command : Message, IRequest<ValidationResult>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }
        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}