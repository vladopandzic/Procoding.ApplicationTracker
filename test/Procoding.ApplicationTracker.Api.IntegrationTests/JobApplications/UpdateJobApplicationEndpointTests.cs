using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class UpdateJobApplicationEndpointTests
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
    public async Task UpdateJobApplication_ShouldUpdateJobApplication()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var candidate = await dbContext.Candidates.FirstAsync();
        var company = await dbContext.Companies.FirstAsync();
        var jobApplicationSource = await dbContext.JobApplicationSources.FirstAsync();
        var allJobApplicationsBefore = await dbContext.JobApplications.ToListAsync();
        var firstFromDb = dbContext.JobApplications.FirstOrDefault();


        //Act
        var response = await client.PutAsJsonAsync($"job-applications",
                                                   new JobApplicationUpdateRequestDTO(Id: firstFromDb!.Id,
                                                                                      CandidateId: candidate.Id,
                                                                                      JobApplicationSourceId: jobApplicationSource.Id,
                                                                                      CompanyId: company.Id));
        var json = await response.Content.ReadFromJsonAsync<JobApplicationUpdatedResponseDTO>();

        //Assert
        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.JobApplication.Candidate.Name, Is.EqualTo(candidate.Name));
        Assert.That(json!.JobApplication.Candidate.Surname, Is.EqualTo(candidate.Surname));
        Assert.That(json!.JobApplication.Candidate.Email, Is.EqualTo(candidate.Email.Value));
        Assert.That(json!.JobApplication.Company.CompanyName, Is.EqualTo(company.CompanyName.Value));
        Assert.That(json!.JobApplication.Company.OfficialWebSiteLink, Is.EqualTo(company.OfficialWebSiteLink.Value));
        Assert.That(json!.JobApplication.ApplicationSource.Name, Is.EqualTo(jobApplicationSource.Name));
    }
}
