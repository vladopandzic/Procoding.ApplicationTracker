using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
public class GetOneJobApplicationSourceEndpointTests : TestBase
{


    [Test]
    public async Task GetOneApplicationSource_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.JobApplicationSources.FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<JobApplicationSourceResponseDTO>($"job-application-sources/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSource.Name, Is.EqualTo(firstFromDb.Name));
    }

    [Test]
    public async Task GetOneApplicationSource_WhenEmployeeLoggedin_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.JobApplicationSources.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<JobApplicationSourceResponseDTO>($"job-application-sources/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSource.Name, Is.EqualTo(firstFromDb.Name));
    }
}
