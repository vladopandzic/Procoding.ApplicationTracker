using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public class TestBase
{

    internal CustomWebApplicationFactory _factory;

    [SetUp]
    public async Task Setup()
    {
        var testDatabaseHelper = new TestDatabaseHelper();
        _factory = new CustomWebApplicationFactory(testDatabaseHelper,
                                                   (x) =>
                                                   {
                                                   });
        _factory.CreateClient();
        var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Candidate>>();
        var employeeUserManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();

        await testDatabaseHelper.SetupDatabase(dbContext, userManager, employeeUserManager);
    }

    [TearDown]
    public async Task TearDown()
    {
        if (_factory is not null)
        {
            await _factory.TestDatabaseHelper.DeleteAsync(_factory.Services.GetRequiredScopedService<ApplicationDbContext>());
            await _factory.DisposeAsync();
        }
    }
}
