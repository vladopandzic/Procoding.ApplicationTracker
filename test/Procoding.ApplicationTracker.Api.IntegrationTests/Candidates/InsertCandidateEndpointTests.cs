using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
internal class InsertCandidateEndpointTests : TestBase
{
    [Test]
    public async Task InsertCandidate_ShouldInsertNewCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidates = await dbContext.Candidates.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"candidates", new CandidateInsertRequestDTO("NameNew", "SurnameNew", "newemail2@newemail.com"));
        var json = await response.Content.ReadFromJsonAsync<CandidateInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidatesAfter = await dbContext2.Candidates.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Candidate.Name, Is.EqualTo("NameNew"));
        Assert.That(json!.Candidate.Surname, Is.EqualTo("SurnameNew"));
        Assert.That(json!.Candidate.Email, Is.EqualTo("newemail2@newemail.com"));
        Assert.That(allCandidatesAfter.Count(), Is.EqualTo(allCandidates.Count() + 1));
    }

    [Test]
    public async Task InsertCandidate_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidates = await dbContext.Candidates.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"candidates", new CandidateInsertRequestDTO("", "", ""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidatesAfter = await dbContext2.Candidates.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(CandidateInsertRequestDTO.Name), "'Name' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateInsertRequestDTO.Surname), "'Surname' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateInsertRequestDTO.Email), "'Email' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateInsertRequestDTO.Email), "'Email' is not a valid email address.");
    }
}