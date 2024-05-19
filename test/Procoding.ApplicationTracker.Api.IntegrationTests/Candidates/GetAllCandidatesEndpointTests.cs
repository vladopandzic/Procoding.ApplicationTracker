using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
public class GetAllCandidatesEndpointTests
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
    public async Task GetAllCandidates_ShouldReturnListOfCandidates()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetFromJsonAsync<CandidateListResponseDTO>("candidates");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Candidates.Count, Is.EqualTo(DatabaseSeedData.GetCandidates().Count));
    }
}