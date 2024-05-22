using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        List<JobApplicationSource> jobApplicationSources = [JobApplicationSource.Create(Guid.Empty, "RemoteOk"),
                                                           JobApplicationSource.Create(Guid.Empty, "MojPosao"),
                                                           JobApplicationSource.Create(Guid.Empty, "ITJobsCroatia")];

        await dbContext.AddRangeAsync(jobApplicationSources);

        List<Company> companies = [Company.Create(new CompanyName("CompanyName Ltd."), new Link("https://www.company.com")),
                                   Company.Create(new CompanyName("CompanyName Ltd. 2"), new Link("https://www.company2.com")),
                                   Company.Create(new CompanyName("CompanyName Ltd. 3"), new Link("https://www.company3.com"))];

        await dbContext.AddRangeAsync(companies);

        List<Candidate> candidates = [Candidate.Create(Guid.Empty, "Name", "Surname", new Email("email@email.com")),
                                      Candidate.Create(Guid.Empty, "Name2", "Surname2", new Email("email2@email.com"))];

        await dbContext.AddRangeAsync(candidates);

        //to that related entities get saved and get an id.
        await dbContext.SaveChangesAsync();

        var candidate = dbContext.Candidates.First();
        var company = dbContext.Companies.First();
        var jobApplicationSource = dbContext.JobApplicationSources.First();

        var jobApplications = Enumerable.Range(1, 10).Select(x => JobApplication.Create(candidate, Guid.NewGuid(), jobApplicationSource, company, TimeProvider.System));

        await dbContext.JobApplications.AddRangeAsync(jobApplications.ToList());

        await dbContext.SaveChangesAsync();

    }
}
