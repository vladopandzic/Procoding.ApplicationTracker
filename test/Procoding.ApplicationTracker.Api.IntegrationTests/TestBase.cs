using Docker.DotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Data;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public class TestBase
{
    private readonly Testcontainers.PostgreSql.PostgreSqlContainer _msSqlContainer
      = new PostgreSqlBuilder().Build();

    internal CustomWebApplicationFactory _factory;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        await _msSqlContainer.StartAsync();
    }
    public async ValueTask<string> CreateDatabaseAndGetConnectionStringAsync()
    {
        PostgreSqlContainer serverInstance = _msSqlContainer;
        string databaseName = "Test" + Guid.NewGuid().ToString().Replace("-", "");
        await serverInstance.ExecScriptAsync($"create database [{databaseName}]");
        string connectionString =
            serverInstance
                .GetConnectionString()
                .Replace("Database=postgres", $"Database={databaseName}");
        return connectionString;
    }

    [SetUp]
    public async Task Setup()
    {


        var testDatabaseHelper = new TestDatabaseHelper();

        var connectionString = await CreateDatabaseAndGetConnectionStringAsync();

        _factory = new CustomWebApplicationFactory(testDatabaseHelper,
                                                   connectionString,
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

    [OneTimeTearDown]
    public async Task TearDownOneTime()
    {

        await _msSqlContainer.DisposeAsync();

    }


}
