using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
namespace Procoding.ApplicationTracker.Api.IntegrationTests;


public static class DatabaseSeedData
{
    public static List<JobApplicationSource> GetJobApplicationSources()
    {
        return [JobApplicationSource.Create(Guid.Empty, "Linkedin")];
    }

    public static List<Candidate> GetCandidates(IPasswordHasher<Candidate> passwordHasher)
    {
        return [Candidate.Create(Guid.Empty,
                                 "Name",
                                 "Surname",
                                 new Email(LoginHelper.DEFAULT_CANDIDATE_USER_EMAIL),
                                 LoginHelper.DEFAULT_CANDIDATE_USER_PASSWORD,
                                 passwordHasher)];
    }

    public static List<Employee> GetEmployees(IPasswordHasher<Employee> passwordHasher)
    {
        return [Employee.Create(Guid.Empty,
                                "Vlado",
                                "Pandzic",
                                new Email(LoginHelper.DEFAULT_EMPLOYEE_USER_EMAIL),
                                LoginHelper.DEFAULT_EMPLOYEE_USER_PASSWORD,
                                passwordHasher)];
    }

    public static List<Company> GetCompanies()
    {
        return [Company.Create(new CompanyName("CompanyName Ltd."), new Link("https://www.company.com"))];
    }

    public static JobApplication GetJobApplication(Candidate candidate,
                                                   Company company,
                                                   JobApplicationSource jobApplicationSource,
                                                   string jobPositionTitle,
                                                   Link jobAdLink,
                                                   WorkLocationType workLocationType,
                                                   JobType jobType,
                                                   string? description = null)
    {
        var jobApplication = JobApplication.Create(candidate: candidate,
                                                   id: Guid.NewGuid(),
                                                   jobApplicationSource: jobApplicationSource,
                                                   company: company,
                                                   timeProvider: TimeProvider.System,
                                                   jobType: jobType,
                                                   workLocationType: workLocationType,
                                                   jobAdLink: jobAdLink,
                                                   jobPositionTitle: jobPositionTitle,
                                                   description: description);
        return jobApplication;
    }
}
