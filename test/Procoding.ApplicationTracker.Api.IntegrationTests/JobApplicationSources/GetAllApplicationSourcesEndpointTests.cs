using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
public class GetAllApplicationSourcesEndpointTests
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
        {
            await _factory.TestDatabaseHelper.DeleteAsync();
            await _factory.DisposeAsync();

        }
    }

    [Test]
    public async Task GetApplicationSource_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();
        //Act
        var response = await client.GetFromJsonAsync<JobApplicationSourceListResponseDTO>("job-application-sources");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSources.Count, Is.EqualTo(DatabaseSeedData.GetJobApplicationSources().Count));
    }
}
