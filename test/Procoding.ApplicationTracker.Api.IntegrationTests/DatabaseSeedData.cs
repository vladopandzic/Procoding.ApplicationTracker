using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
namespace Procoding.ApplicationTracker.Api.IntegrationTests;


public static class DatabaseSeedData
{
    public static List<JobApplicationSource> GetJobApplicationSources()
    {
        return [JobApplicationSource.Create(Guid.Empty, "Linkedin")];
    }

    public static List<Candidate> GetCandidates()
    {
        return [Candidate.Create(Guid.Empty, "Name", "Surname", new Email("email@email.com"))];
    }

    public static List<Company> GetCompanies()
    {
        return [Company.Create(new CompanyName("CompanyName Ltd."), new Link("https://www.company.com"))];
    }

    public static JobApplication GetJobApplication(Candidate candidate, Company company, JobApplicationSource jobApplicationSource)
    {

        var jobApplication = JobApplication.Create(candidate, Guid.NewGuid(), jobApplicationSource, company, TimeProvider.System);
        return jobApplication;
    }
}
