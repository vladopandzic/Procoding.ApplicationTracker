using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
internal class UpdateJobApplicationSourceEndpointTests
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
    public async Task UpdateJobApplicationSource_ShouldUpdateItsName()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.JobApplicationSources.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"job-application-sources", new UpdateJobApplicationSourceCommand(firstFromDb!.Id, "NewName"));
        var json = await response.Content.ReadFromJsonAsync<JobApplicationSourceUpdatedResponseDTO>();
     
        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.JobApplicationSource.Name, Is.EqualTo("NewName"));
        Assert.That(firstFromDb.Name, Is.Not.EqualTo("NewName"));
    }

    [Test]
    public async Task Update_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompanies = await dbContext.Companies.ToListAsync();
        var firstFromDb = dbContext.JobApplicationSources.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"job-application-sources", new JobApplicationSourceUpdateRequestDTO(firstFromDb!.Id, ""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobApplicationSources = await dbContext2.JobApplicationSources.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(JobApplicationSourceUpdateRequestDTO.Name), "'Name' must not be empty.");
    }
}
