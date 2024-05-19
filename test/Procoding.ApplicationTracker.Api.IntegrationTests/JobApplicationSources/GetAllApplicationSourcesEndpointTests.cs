using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
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
            await _factory.DisposeAsync();
    }

    [Test]
    public async Task GetApplicationSource_ShouldReturnListOfJobAplicationSources()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();
        //Act
        var response = await client.GetFromJsonAsync<CompanyListResponseDTO>("companies");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Companies.Count, Is.EqualTo(DatabaseSeedData.GetCompanies().Count));
    }
}
