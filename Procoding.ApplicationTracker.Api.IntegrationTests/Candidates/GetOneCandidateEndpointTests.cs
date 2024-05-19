using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
public class GetOneCandidateEndpointTests
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
    public async Task GetOneCandidateEndpointTests_ShouldReturnCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Candidates.FirstOrDefault();

        //Act
        var response = await client.GetFromJsonAsync<CandidateResponseDTO>($"candidates/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Candidate.Name, Is.EqualTo(firstFromDb.Name));
        Assert.That(response.Candidate.Surname, Is.EqualTo(firstFromDb.Surname));
        Assert.That(response.Candidate.Email, Is.EqualTo(firstFromDb.Email.Value));


    }
}
