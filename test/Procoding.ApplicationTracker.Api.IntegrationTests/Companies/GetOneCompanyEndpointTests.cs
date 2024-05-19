using Procoding.ApplicationTracker.DTOs.Model;
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
public class GetOneCompanyEndpointTests
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
    public async Task GetOneCompanyEndpointTests_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        var response = await client.GetFromJsonAsync<CompanyResponseDTO>($"companies/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Company.Name, Is.EqualTo(firstFromDb.CompanyName.Value));
        Assert.That(response.Company.OfficialWebSiteLink, Is.EqualTo(firstFromDb.OfficialWebSiteLink.Value));

    }
}
