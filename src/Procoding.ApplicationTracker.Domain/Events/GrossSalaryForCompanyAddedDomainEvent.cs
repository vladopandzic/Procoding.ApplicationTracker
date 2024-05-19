using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Events
{
    public sealed class GrossSalaryForCompanyAddedDomainEvent : IDomainEvent
    {
        public GrossSalaryForCompanyAddedDomainEvent(Guid id, CompanyName companyName, CompanyAverageGrossSalary companyAverageGrossSalary)
        {
            Id = id;
            CompanyName = companyName;
            CompanyAverageGrossSalary = companyAverageGrossSalary;
        }

        public Guid Id { get; }

        public CompanyName CompanyName { get; }

        public CompanyAverageGrossSalary CompanyAverageGrossSalary { get; }
    }
}
