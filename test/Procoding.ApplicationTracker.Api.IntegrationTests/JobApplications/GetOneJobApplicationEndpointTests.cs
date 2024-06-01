using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class GetOneJobApplicationEndpointTests
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
        {
            await _factory.TestDatabaseHelper.DeleteAsync();
            await _factory.DisposeAsync();

        }
    }

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
                                   .FirstOrDefault();

        //Act
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
