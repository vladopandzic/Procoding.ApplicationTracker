using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Companies;

[TestFixture]
public class GetAllCompaniesEndpointTests : TestBase
{
    [Test]
    public async Task GetAllCompanies_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetFromJsonAsync<CompanyListResponseDTO>("companies");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Companies.Count, Is.EqualTo(DatabaseSeedData.GetCompanies().Count));
    }

    [Test]
    public async Task GetAllCompanies_WhenLoggedInAsCandidate_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<CompanyListResponseDTO>("companies");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Companies.Count, Is.EqualTo(DatabaseSeedData.GetCompanies().Count));
    }
}