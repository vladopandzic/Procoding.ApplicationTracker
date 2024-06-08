using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class GetOneJobApplicationEndpointTests : TestBase
{
    [Test]
    public async Task GetOneJobApplicationEndpointTests_ShouldReturnListOfJobApplications()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.JobApplications
                                   .Include(x => x.Company)
                                   .Include(x => x.Candidate)
                                   .Include(x => x.ApplicationSource)
                                   .IgnoreQueryFilters()
                                   .FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<JobApplicationResponseDTO>($"job-applications/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplication.ApplicationSource.Name, Is.EqualTo(firstFromDb.ApplicationSource.Name));
        Assert.That(response.JobApplication.Company.CompanyName, Is.EqualTo(firstFromDb.Company.CompanyName.Value));
        Assert.That(response.JobApplication.Company.OfficialWebSiteLink, Is.EqualTo(firstFromDb.Company.OfficialWebSiteLink.Value));
        Assert.That(response.JobApplication.Candidate.Name, Is.EqualTo(firstFromDb.Candidate.Name));
        Assert.That(response.JobApplication.Candidate.Surname, Is.EqualTo(firstFromDb.Candidate.Surname));
        Assert.That(response.JobApplication.Candidate.Email, Is.EqualTo(firstFromDb.Candidate.Email.Value));
    }
}
