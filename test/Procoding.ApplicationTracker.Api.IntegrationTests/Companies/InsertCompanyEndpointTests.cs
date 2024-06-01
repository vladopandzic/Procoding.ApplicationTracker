using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Companies;

[TestFixture]
internal class InsertCompanyEndpointTests
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
    public async Task InsertJobApplicationSource_ShouldInsertNewJobApplicationSource()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompanies = await dbContext.Companies.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"companies", new CompanyInsertRequestDTO("CompanyName","https://www.company.hr"));
        var json = await response.Content.ReadFromJsonAsync<CompanyInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompaniesAfter = await dbContext2.Companies.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Company.CompanyName, Is.EqualTo("CompanyName"));
        Assert.That(json!.Company.OfficialWebSiteLink, Is.EqualTo("https://www.company.hr"));

        Assert.That(allCompaniesAfter.Count(), Is.EqualTo(allCompanies.Count() + 1));
    }

    [Test]
    public async Task InsertCompany_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompanies = await dbContext.Companies.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"companies", new CompanyInsertRequestDTO("", ""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompaniesAfter = await dbContext2.Companies.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(CompanyInsertRequestDTO.Name), "'Name' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CompanyInsertRequestDTO.OfficialWebSiteLink), "'Official Web Site Link' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(CompanyInsertRequestDTO.OfficialWebSiteLink), "'Official Web Site Link' is not a valid URL.");
    }
}