using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
public class GetOneJobApplicationSourceEndpointTests
{
    private CustomWebApplicationFactory _factory;

    [SetUp]
    public async Task Setup()
    {
        var testDatabaseHelper = new TestDatabaseHelper();
        await testDatabaseHelper.SetupDatabase();
        _factory = new CustomWebApplicationFactory(testDatabaseHelper,
                                                   (x) =>
        {
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        if (_factory is not null)
            await _factory.DisposeAsync();
    }

    [Test]
    public async Task GetOneApplicationSource_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.JobApplicationSources.FirstOrDefault();

        //Act
        var response = await client.GetFromJsonAsync<JobApplicationSourceResponseDTO>($"job-application-sources/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSource.Name, Is.EqualTo(firstFromDb.Name));
    }
}
