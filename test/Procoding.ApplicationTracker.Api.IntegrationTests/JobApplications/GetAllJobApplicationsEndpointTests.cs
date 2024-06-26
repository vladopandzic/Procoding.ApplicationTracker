﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.JobApplications;

[TestFixture]
public class GetAllJobApplicationsEndpointTests : TestBase
{
    [Test]
    public async Task GetAllJobApplications_ShouldReturnListOfJobApplications()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<JobApplicationListResponseDTO>("job-applications");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplications.Count, Is.EqualTo(1));
    }


    [Test]
    public async Task GetAllJobApplications_ShouldSupportPagination()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var candidate = await dbContext.Candidates.FirstAsync();
        var company = await dbContext.Companies.FirstAsync();
        var jobApplicationSource = await dbContext.JobApplicationSources.FirstAsync();

        await dbContext.JobApplications.AddRangeAsync(GenerateJobApplications(50, candidate, jobApplicationSource, company));
        await dbContext.SaveChangesAsync();

        //Act
        var newRequest = new EmployeeGetListRequestDTO()
        {
            PageNumber = 1,
            PageSize = 10
        };
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<JobApplicationListResponseDTO>($"job-applications?{newRequest.ToQueryString()}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplications.Count, Is.EqualTo(10));
        Assert.That(response.TotalCount, Is.EqualTo(51));
    }

    public static List<JobApplication> GenerateJobApplications(int count, Candidate candidate, JobApplicationSource jobApplicationSource, Company company)
    {
        var faker = new Faker<JobApplication>().CustomInstantiator(f => JobApplication.Create(candidate: candidate,
                                                                                              id: Guid.NewGuid(),
                                                                                              jobApplicationSource: jobApplicationSource,
                                                                                              company: company,
                                                                                              jobPositionTitle: "ss",
                                                                                              jobAdLink: new Link(f.Internet.Url()),
                                                                                              jobType: JobType.Contract,
                                                                                              workLocationType: WorkLocationType.Remote,
                                                                                              description: null,
                                                                                              timeProvider: TimeProvider.System));


        return faker.Generate(count);
    }
}