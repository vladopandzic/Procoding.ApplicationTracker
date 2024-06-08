using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
public class GetOneCandidateEndpointTests : TestBase
{
    [Test]
    public async Task GetOneCandidateEndpointTests_ShouldReturnCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Candidates.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<CandidateResponseDTO>($"candidates/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Candidate.Name, Is.EqualTo(firstFromDb.Name));
        Assert.That(response.Candidate.Surname, Is.EqualTo(firstFromDb.Surname));
        Assert.That(response.Candidate.Email, Is.EqualTo(firstFromDb.Email.Value));
    }

    [Test]
    public async Task GetOneCandidateEndpointTests_WhenCandidateLoggedin_ShouldReturnCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Candidates.FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetAsync($"candidates/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }
}
