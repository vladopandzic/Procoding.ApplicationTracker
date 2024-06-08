using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplicationSources;

[TestFixture]
internal class InsertJobApplicationSourceEndpointTests : TestBase
{

    [Test]
    public async Task InsertJobApplicationSource_ShouldInsertNewJobApplicationSource()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobsSources = await dbContext.JobApplicationSources.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"job-application-sources", new JobApplicationSourceInsertRequestDTO("SomeName"));
        var json = await response.Content.ReadFromJsonAsync<JobApplicationSourceInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobsSourcesAfter = await dbContext2.JobApplicationSources.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.JobApplicationSource.Name, Is.EqualTo("SomeName"));
        Assert.That(allJobsSourcesAfter.Count(), Is.EqualTo(allJobsSources.Count() + 1));
    }

    [Test]
    public async Task InsertJobApplicationSource_IfCandidateLoggedin_ShouldInsertNewJobApplicationSource()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobsSources = await dbContext.JobApplicationSources.ToListAsync();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.PostAsync($"job-application-sources", JsonContent.Create(new JobApplicationSourceInsertRequestDTO("SomeName")));

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task InsertJobApplicationSource_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var jobApplicationSourceS = await dbContext.JobApplicationSources.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"job-application-sources", new JobApplicationSourceInsertRequestDTO(""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var jobApplicationSourcesAfter = await dbContext2.JobApplicationSources.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(JobApplicationSourceInsertRequestDTO.Name), "'Name' must not be empty.");
    }
}