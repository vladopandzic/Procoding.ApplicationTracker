using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Auth;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class UpdateJobApplicationEndpointTests : TestBase
{
    [Test]
    public async Task UpdateJobApplication_ShouldUpdateJobApplication()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var identityContext = _factory.Services.GetRequiredScopedService<IIdentityContext>();
        var candidate = await dbContext.Candidates.FirstAsync();
        var company = await dbContext.Companies.FirstAsync();
        var jobApplicationSource = await dbContext.JobApplicationSources.FirstAsync();
        var allJobApplicationsBefore = await dbContext.JobApplications.ToListAsync();
        var firstFromDb = dbContext.JobApplications.IgnoreQueryFilters().FirstOrDefault();//because we use ApplicationDbContext of different scope


        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.PutAsJsonAsync($"job-applications",
                                                   new JobApplicationUpdateRequestDTO(Id: firstFromDb!.Id,
                                                                                      JobApplicationSourceId: jobApplicationSource.Id,
                                                                                      CompanyId: company.Id,
                                                                                      JobPositionTitle: "Senior .NET engineer",
                                                                                      JobAdLink: "https://www.link.com",
                                                                                      JobType: new DTOs.Model.JobTypeDTO(JobType.Contract.Value),
                                                                                      WorkLocationType: new DTOs.Model.WorkLocationTypeDTO(WorkLocationType.Remote.Value),
                                                                                      Description: null));
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
