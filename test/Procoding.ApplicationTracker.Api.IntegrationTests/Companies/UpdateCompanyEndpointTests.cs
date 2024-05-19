using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
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
internal class UpdateCompanyEndpointTests
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
    public async Task UpdateCompany_ShouldUpdateCompany()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"companies", new CompanyUpdateRequestDTO(firstFromDb!.Id, "NewName","https://www.newLink.com"));
        var json = await response.Content.ReadFromJsonAsync<CompanyUpdatedResponseDTO>();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Company.Name, Is.EqualTo("NewName"));
        Assert.That(firstFromDb.CompanyName.Value, Is.Not.EqualTo("NewName"));
        Assert.That(json!.Company.OfficialWebSiteLink, Is.EqualTo("https://www.newLink.com"));
        Assert.That(firstFromDb.OfficialWebSiteLink.Value, Is.Not.EqualTo("https://www.newLink.com"));
    }
}