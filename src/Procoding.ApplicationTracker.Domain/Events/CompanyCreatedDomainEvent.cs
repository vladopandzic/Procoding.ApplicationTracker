using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Events
{
    /// <summary>
    /// Represents event which happens when new <see cref="Entities.Company"/> is created.
    /// </summary>
    public sealed class CompanyCreatedDomainEvent : IDomainEvent
    {
        public CompanyCreatedDomainEvent(Guid id, CompanyName companyName)
        {
            Id = id;
            Name = companyName;
        }

        public Guid Id { get; }

        public CompanyName Name { get; }
    }
}
