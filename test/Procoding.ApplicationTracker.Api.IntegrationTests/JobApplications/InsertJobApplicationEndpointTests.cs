﻿using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
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
public class InsertJobApplicationEndpointTests
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
    public async Task InsertJobApplication_ShouldInsertNewJobApplication()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var candidate = await dbContext.Candidates.FirstAsync();
        var company = await dbContext.Companies.FirstAsync();
        var jobApplicationSource = await dbContext.JobApplicationSources.FirstAsync();
        var allJobApplicationsBefore = await dbContext.JobApplications.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"job-applications",
                                                    new JobApplicationInsertRequestDTO(CandidateId: candidate.Id,
                                                                                       JobApplicationSourceId: jobApplicationSource.Id,
                                                                                       CompanyId: company.Id));
        var json = await response.Content.ReadFromJsonAsync<JobApplicationInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allJobApplicationsAfter = await dbContext2.JobApplications.ToListAsync();

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
