using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;

[TestFixture]
public class GetOneEmployeeEndpointTests : TestBase
{

    [Test]
    public async Task GetOneEmployeeEndpointTests_ShouldReturnEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Employees.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<EmployeeResponseDTO>($"employees/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employee.Name, Is.EqualTo(firstFromDb.Name));
        Assert.That(response.Employee.Surname, Is.EqualTo(firstFromDb.Surname));
        Assert.That(response.Employee.Email, Is.EqualTo(firstFromDb.Email.Value));
    }

    [Test]
    public async Task GetOneEmployeeEndpointTests_WhenLoggedinAsCandidate_ShouldReturnForbidden()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Employees.FirstOrDefault();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetAsync($"employees/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }
}
