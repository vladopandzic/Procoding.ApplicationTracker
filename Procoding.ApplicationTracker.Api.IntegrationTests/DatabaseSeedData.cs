using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
namespace Procoding.ApplicationTracker.Api.IntegrationTests;


public static class DatabaseSeedData
{
    public static List<JobApplicationSource> GetJobApplicationSources()
    {
        return [JobApplicationSource.Create(Guid.Empty, "Linkedin")];
    }

    public static List<Company> GetCompanies()
    {
        return [Company.Create(new CompanyName("CompanyName Ltd."), new Link("https://www.company.com"))];
    }
}
