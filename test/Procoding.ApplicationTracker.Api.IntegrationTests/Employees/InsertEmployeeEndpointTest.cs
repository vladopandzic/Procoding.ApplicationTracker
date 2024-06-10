using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;

[TestFixture]
internal class InsertEmployeeEndpointTests : TestBase
{
    [Test]
    public async Task InsertEmployee_ShouldInsertNewEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"employees",
                                                    new EmployeeInsertRequestDTO("NameNew", "SurnameNew", "newemail@newemail.com", "Pass123!", true));
        var json = await response.Content.ReadFromJsonAsync<EmployeeInsertedResponseDTO>();
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployeesAfter = await dbContext2.Employees.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Employee.Name, Is.EqualTo("NameNew"));
        Assert.That(json!.Employee.Surname, Is.EqualTo("SurnameNew"));
        Assert.That(json!.Employee.Email, Is.EqualTo("newemail@newemail.com"));
        Assert.That(allEmployeesAfter.Count(), Is.EqualTo(allEmployees.Count() + 1));
    }

    [Test]
    public async Task InsertEmployee_IfLoggedInAsCandidate_ShouldInsertNewEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.PostAsync($"employees",
                                              JsonContent.Create(new EmployeeInsertRequestDTO("NameNew",
                                                                                              "SurnameNew",
                                                                                              "newemail@newemail.com",
                                                                                              "Pass123!",
                                                                                              true)));

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }


    [Test]
    public async Task InsertEmployee_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PostAsJsonAsync($"employees", new EmployeeInsertRequestDTO("", "", "", "", true));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployeesAfter = await dbContext2.Employees.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(EmployeeInsertRequestDTO.Name), "'Name' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeInsertRequestDTO.Surname), "'Surname' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeInsertRequestDTO.Email), "'Email' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeInsertRequestDTO.Email), "'Email' is not a valid email address.");
    }
}