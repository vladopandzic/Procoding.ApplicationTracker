using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
public class GetAllApplicationSourcesEndpointTests : TestBase
{
    [Test]
    public async Task GetApplicationSource_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();
       
        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<JobApplicationSourceListResponseDTO>("job-application-sources");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSources.Count, Is.EqualTo(DatabaseSeedData.GetJobApplicationSources().Count));
    }

    [Test]
    public async Task GetApplicationSource_WhenEmployeeVisits_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();
       
        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<JobApplicationSourceListResponseDTO>("job-application-sources");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSources.Count, Is.EqualTo(DatabaseSeedData.GetJobApplicationSources().Count));
    }
}
