using Procoding.ApplicationTracker.Domain.Events;

namespace Procoding.ApplicationTracker.Domain.Common
{
    public interface IAggregateRoot
    {
        /// <summary>
        /// Clears all the domain events from the <see cref="IAggregateRoot"/>.
        /// </summary>
        void ClearDomainEvents();

        /// <summary>
        /// Gets the domain events. This collection is readonly.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
