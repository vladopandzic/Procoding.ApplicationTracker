using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Events
{
    /// <summary>
    /// Represents event which happens when new <see cref="Entities.Company"/> is updated.
    /// </summary>
    public sealed class CompanyUpdatedDomainEvent : IDomainEvent
    {
        public CompanyUpdatedDomainEvent(Guid id, CompanyName companyName, Link officialWebSiteLink)
        {
            Id = id;
            CompanyName = companyName;
            OfficialWebSiteLink = officialWebSiteLink;
        }

        public Guid Id { get; }

        public CompanyName CompanyName { get; }

        public Link OfficialWebSiteLink { get; }
    }
}
