using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Companies;

[TestFixture]
public class GetOneCompanyEndpointTests : TestBase
{
    [Test]
    public async Task GetOneCompanyEndpointTests_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<CompanyResponseDTO>($"companies/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Company.CompanyName, Is.EqualTo(firstFromDb.CompanyName.Value));
        Assert.That(response.Company.OfficialWebSiteLink, Is.EqualTo(firstFromDb.OfficialWebSiteLink.Value));
    }

    [Test]
    public async Task GetOneCompanyEndpointTests_WhenLoggedInAsEmployee_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<CompanyResponseDTO>($"companies/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Company.CompanyName, Is.EqualTo(firstFromDb.CompanyName.Value));
        Assert.That(response.Company.OfficialWebSiteLink, Is.EqualTo(firstFromDb.OfficialWebSiteLink.Value));
    }
}
