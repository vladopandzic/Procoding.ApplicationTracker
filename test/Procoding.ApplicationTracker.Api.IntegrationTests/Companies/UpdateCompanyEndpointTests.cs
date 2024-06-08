using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Companies;

[TestFixture]
internal class UpdateCompanyEndpointTests : TestBase
{
    [Test]
    public async Task UpdateCompany_ShouldUpdateCompany()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PutAsJsonAsync($"companies", new CompanyUpdateRequestDTO(firstFromDb!.Id, "NewName", "https://www.newLink.com"));
        var json = await response.Content.ReadFromJsonAsync<CompanyUpdatedResponseDTO>();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Company.CompanyName, Is.EqualTo("NewName"));
        Assert.That(firstFromDb.CompanyName.Value, Is.Not.EqualTo("NewName"));
        Assert.That(json!.Company.OfficialWebSiteLink, Is.EqualTo("https://www.newLink.com"));
        Assert.That(firstFromDb.OfficialWebSiteLink.Value, Is.Not.EqualTo("https://www.newLink.com"));
    }

    [Test]
    public async Task UpdateCompany_WhenLoggedInAsCandaite_ShouldReturnForbidden()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.PutAsync($"companies",
                                             JsonContent.Create(new CompanyUpdateRequestDTO(firstFromDb!.Id, "NewName", "https://www.newLink.com")));

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task Update_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allCompanies = await dbContext.Companies.ToListAsync();
        var firstFromDb = dbContext.Companies.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PutAsJsonAsync($"companies", new CompanyUpdateRequestDTO(firstFromDb!.Id, "", ""));
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