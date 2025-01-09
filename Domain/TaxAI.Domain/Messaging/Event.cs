using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using TaxAI.Domain.Interfaces;

namespace TaxAI.Domain.Messaging
{
    [NotMapped]
    [Serializable]
    public class Event : Message, INotification, IAggregateRoot
    {
        public Guid Id { get; set; }
        public DateTime Timestamp
        {
            get;
            private set;
        }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}