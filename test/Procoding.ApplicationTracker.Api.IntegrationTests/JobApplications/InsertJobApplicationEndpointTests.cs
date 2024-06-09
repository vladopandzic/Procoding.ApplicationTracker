using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class InsertJobApplicationEndpointTests : TestBase
{
    [Test]
    public async Task InsertJobApplication_ShouldInsertNewJobApplication()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var candidate = await dbContext.Candidates.FirstAsync();
        var company = await dbContext.Companies.FirstAsync();
        var jobApplicationSource = await dbContext.JobApplicationSources.FirstAsync();
        var allJobApplicationsBefore = await dbContext.JobApplications.IgnoreQueryFilters().ToListAsync();

        //Act
        await LoginHelper.LoginCandidate(client);
        var responseLogin = await client.PostAsJsonAsync("candidates/login",
                                                         new CandidateLoginRequestDTO()
                                                         {
                                                             Email = "email@email.com",
                                                             Password = "test123"
                                                         });
        var response = await client.PostAsJsonAsync($"job-applications",
                                                    new JobApplicationInsertRequestDTO(JobApplicationSourceId: jobApplicationSource.Id,
                                                                                       CompanyId: company.Id,
                                                                                       JobPositionTitle: "Senior .NET engineer",
                                                                                       JobAdLink: "https://www.link.com",
                                                                                       JobType: new DTOs.Model.JobTypeDTO(JobType.Contract.Value),
                                                                                       WorkLocationType: new DTOs.Model.WorkLocationTypeDTO(WorkLocationType.Remote.Value),
                                                                                       Description: null));
        var json = await response.Content.ReadFromJsonAsync<JobApplicationInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobApplicationsAfter = await dbContext2.JobApplications.IgnoreQueryFilters().ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(allJobApplicationsAfter.Count(), Is.EqualTo(allJobApplicationsBefore.Count() + 1));

        Assert.That(json!.JobApplication.Candidate.Name, Is.EqualTo(candidate.Name));
        Assert.That(json!.JobApplication.Candidate.Surname, Is.EqualTo(candidate.Surname));
        Assert.That(json!.JobApplication.Candidate.Email, Is.EqualTo(candidate.Email.Value));
        Assert.That(json!.JobApplication.Company.CompanyName, Is.EqualTo(company.CompanyName.Value));
        Assert.That(json!.JobApplication.Company.OfficialWebSiteLink, Is.EqualTo(company.OfficialWebSiteLink.Value));
        Assert.That(json!.JobApplication.ApplicationSource.Name, Is.EqualTo(jobApplicationSource.Name));
    }
}
