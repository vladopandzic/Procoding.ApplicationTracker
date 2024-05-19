using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public class TestDatabaseHelper
{
    private readonly string _databaseName;
    private readonly string _connectionString;
    private readonly DbContextOptions<ApplicationDbContext> _options;
    private const string DATABASE_SERVER_NAME = "localhost\\SQLEXPRESS";

    public TestDatabaseHelper()
    {
        _databaseName = $"TestDb_{Guid.NewGuid()}";
        _connectionString = $"Server={DATABASE_SERVER_NAME};Database={_databaseName};Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                                                    .UseSqlServer(_connectionString)
                                                                    .Options;
    }

    public DbContextOptions<ApplicationDbContext> GetDbContextOptions() => _options;

    public async Task SetupDatabase()
    {
        using var context = new ApplicationDbContext(_options, TimeProvider.System);
        context.Database.EnsureCreated();
        await SeedDatabaseAsync(context);
    }

    private async Task SeedDatabaseAsync(ApplicationDbContext context)
    {
        await context.JobApplicationSources.AddRangeAsync(DatabaseSeedData.GetJobApplicationSources());
        await context.Companies.AddRangeAsync(DatabaseSeedData.GetCompanies());

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync()
    {
        using var context = new ApplicationDbContext(_options, TimeProvider.System);
        await context.Database.EnsureDeletedAsync();
    }
}