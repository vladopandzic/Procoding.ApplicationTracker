using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobTypes;


[TestFixture]
public class GetAllJobTypesEndpointTests : TestBase
{
    [Test]
    public async Task GetJobTypes_ShouldReturnListOfJobTypes()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();

        //Act
        var response = await client.GetFromJsonAsync<JobTypeListResponseDTO>("job-types");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobTypes.Count, Is.EqualTo(JobType.GetAll().Count()));
    }
}
