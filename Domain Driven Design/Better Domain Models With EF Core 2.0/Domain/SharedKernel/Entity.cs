using System.Collections.Generic;

namespace Domain.SharedKernel
{
    public abstract class Entity
    {
        private List<IEvent> _domainEvents;
        public IEnumerable<IEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IEvent @event)
        {
            _domainEvents = _domainEvents ?? new List<IEvent>();
            _domainEvents.Add(@event);
        }

        protected void RemoveDomainEvent(IEvent @event)
        {
            if (_domainEvents is null)
                return;

            _domainEvents.Remove(@event);
        }
    }
}