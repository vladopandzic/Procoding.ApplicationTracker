using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public class TestDatabaseHelper
{
    private readonly string _databaseName;

    public string ConnectionString { get; }

    private readonly DbContextOptions<ApplicationDbContext> _options;
    private const string DATABASE_SERVER_NAME = "localhost\\SQLEXPRESS";

    public TestDatabaseHelper()
    {
        _databaseName = $"TestDb2_{Guid.NewGuid()}";
        ConnectionString = $"Server={DATABASE_SERVER_NAME};Database={_databaseName};Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                                                    .UseSqlServer(ConnectionString)
                                                                    .Options;
    }

    public DbContextOptions<ApplicationDbContext> GetDbContextOptions() => _options;


    public async Task SetupDatabase(ApplicationDbContext applicationDbContext, UserManager<Candidate> userManager, UserManager<Employee> employeeUserManager)
    {
        using var context = applicationDbContext;
        context.Database.EnsureCreated();
        await SeedDatabaseAsync(context, userManager, employeeUserManager);
    }

    private async Task SeedDatabaseAsync(ApplicationDbContext context, UserManager<Candidate> candidateUserManager, UserManager<Employee> employeeUserManager)
    {
        await context.JobApplicationSources.AddRangeAsync(DatabaseSeedData.GetJobApplicationSources());

        foreach (var candidate in DatabaseSeedData.GetCandidates(candidateUserManager.PasswordHasher))
        {
            var result = await candidateUserManager.CreateAsync(candidate);

            if (!result.Succeeded)
            {
                throw new Exception("Not seeded candidate");
            }
        }

        foreach (var employee in DatabaseSeedData.GetEmployees(employeeUserManager.PasswordHasher))
        {
            var result = await employeeUserManager.CreateAsync(employee);

            if (!result.Succeeded)
            {
                throw new Exception("Not seeded employee");
            }
        }

        await context.Companies.AddRangeAsync(DatabaseSeedData.GetCompanies());


        await context.SaveChangesAsync();

        JobApplication jobApplication = DatabaseSeedData.GetJobApplication(context.Candidates.First(),
                                                                           context.Companies.First(),
                                                                           context.JobApplicationSources.First(),
                                                                           jobPositionTitle: "Senior .NET sw engineer",
                                                                           jobAdLink: new Link("https://www.link2.com"),
                                                                           workLocationType: WorkLocationType.Remote,
                                                                           jobType: JobType.FullTime);
        await context.JobApplications.AddAsync(jobApplication);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ApplicationDbContext applicationDbContext)
    {
        using var context = applicationDbContext;
        await context.Database.EnsureDeletedAsync();
    }
}