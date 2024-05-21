using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
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
internal class UpdateCandidateEndpointTests
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
    public async Task UpdateCandidate_ShouldUpdateCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Candidates.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"candidates",
                                                   new UpdateCandidateCommand(firstFromDb!.Id, "UpdatedName", "UpdatedSurname", "updatedEmail@email.com"));
        var json = await response.Content.ReadFromJsonAsync<CandidateUpdatedResponseDTO>();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Candidate.Name, Is.EqualTo("UpdatedName"));
        Assert.That(firstFromDb.Name, Is.Not.EqualTo("UpdatedName"));
        Assert.That(json!.Candidate.Surname, Is.EqualTo("UpdatedSurname"));
        Assert.That(firstFromDb.Surname, Is.Not.EqualTo("UpdatedSurname"));
        Assert.That(json!.Candidate.Email, Is.EqualTo("updatedEmail@email.com"));
        Assert.That(firstFromDb.Email.Value, Is.Not.EqualTo("updatedEmail@email.com"));
    }

    [Test]
    public async Task Update_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidates = await dbContext.Candidates.ToListAsync();
        var firstFromDb = dbContext.Candidates.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"candidates", new CandidateUpdateRequestDTO(firstFromDb!.Id, "", "", ""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidatesAfter = await dbContext2.Candidates.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(CandidateUpdateRequestDTO.Name), "'Name' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateUpdateRequestDTO.Surname), "'Surname' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateUpdateRequestDTO.Email), "'Email' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CandidateUpdateRequestDTO.Email), "'Email' is not a valid email address.");
    }
}