using System.Text.Json.Serialization;
using TaxAI.Domain.Messaging;

namespace TaxAI.Domain
{
    [Serializable]
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }

        [NonSerialized]
        private List<Event> _domainEvents;

        [JsonIgnore]
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<Event>();
            _domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(null, compareTo)) return true;
            if (ReferenceEquals(this, compareTo)) return true;
            return Id.Equals(compareTo.Id);
        }
        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
