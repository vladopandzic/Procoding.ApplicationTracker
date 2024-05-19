using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
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
internal class InsertCandidateEndpointTests
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
    public async Task InsertCandidate_ShouldInsertNewCandidate()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidates = await dbContext.Candidates.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"candidates", new CandidateInsertRequestDTO("NameNew", "SurnameNew","newemail@newemail.com"));
        var json = await response.Content.ReadFromJsonAsync<CandidateInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCandidatesAfter = await dbContext2.Candidates.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Candidate.Name, Is.EqualTo("NameNew"));
        Assert.That(json!.Candidate.Surname, Is.EqualTo("SurnameNew"));
        Assert.That(json!.Candidate.Email, Is.EqualTo("newemail@newemail.com"));
        Assert.That(allCandidatesAfter.Count(), Is.EqualTo(allCandidates.Count() + 1));
    }
}