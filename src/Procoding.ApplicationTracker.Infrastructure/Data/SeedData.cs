using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext dbContext, IPasswordHasher<Candidate> passwordHasher)
    {
        List<JobApplicationSource> jobApplicationSources = [JobApplicationSource.Create(Guid.Empty, "RemoteOk"),
                                                           JobApplicationSource.Create(Guid.Empty, "MojPosao"),
                                                           JobApplicationSource.Create(Guid.Empty, "ITJobsCroatia")];

        await dbContext.AddRangeAsync(jobApplicationSources);

        List<Company> companies = [Company.Create(new CompanyName("CompanyName Ltd."), new Link("https://www.company.com")),
                                   Company.Create(new CompanyName("CompanyName Ltd. 2"), new Link("https://www.company2.com")),
                                   Company.Create(new CompanyName("CompanyName Ltd. 3"), new Link("https://www.company3.com"))];

        await dbContext.AddRangeAsync(companies);

        List<Candidate> candidates = [Candidate.Create(Guid.Empty, "Name", "Surname", new Email("email@email.com"), "test123", passwordHasher),
                                      Candidate.Create(Guid.Empty, "Name2", "Surname2", new Email("email2@email.com"), "test123", passwordHasher)];

        await dbContext.AddRangeAsync(candidates);

        //to that related entities get saved and get an id.
        await dbContext.SaveChangesAsync();

        var candidate = dbContext.Candidates.Skip(1).Take(1).First();
        var company = dbContext.Companies.Skip(1).Take(1).First();
        var jobApplicationSource = dbContext.JobApplicationSources.Skip(1).Take(1).First();

        var jobApplications = Enumerable.Range(1, 10).Select(x => JobApplication.Create(candidate: candidate,
                                                                                        id: Guid.NewGuid(),
                                                                                        jobApplicationSource: jobApplicationSource,
                                                                                        company: company,
                                                                                        timeProvider: TimeProvider.System,
                                                                                        jobPositionTitle: "Senior .NET sw engineer",
                                                                                        jobAdLink: new Link("https://www.link2.com"),
                                                                                        workLocationType: WorkLocationType.Remote,
                                                                                        jobType: JobType.FullTime,
                                                                                        description: "desc"));

        await dbContext.JobApplications.AddRangeAsync(jobApplications.ToList());

        await dbContext.SaveChangesAsync();
    }

    public static async Task SeedEmployee(ApplicationDbContext applicationDbContext, UserManager<Employee> userManager)
    {
        var employee = Employee.Create(Guid.NewGuid(),
                                                      "Vlado",
                                                      "Pandžić",
                                                      new Email("pandzic.vlado@gmail.com"),
                                                      "Pass123!",
                                                      userManager.PasswordHasher);

        var result = await userManager.CreateAsync(employee, "Pass123!");

        await applicationDbContext.Employees.AddAsync(employee);

        var result2 = await applicationDbContext.SaveChangesAsync();

    }
}
