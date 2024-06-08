using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;


[TestFixture]
internal class UpdateEmployeeEndpointTests : TestBase
{
    [Test]
    public async Task UpdateEmployee_ShouldUpdateEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Employees.FirstOrDefault();
        var passwordHasher = _factory.Services.GetRequiredScopedService<IPasswordHasher<Employee>>();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PutAsJsonAsync($"employees",
                                                   new EmployeeUpdateRequestDTO(firstFromDb!.Id, "UpdatedName", "UpdatedSurname", "updatedEmail@email.com", "tEST123!!!"));
        var json = await response.Content.ReadFromJsonAsync<EmployeeUpdatedResponseDTO>();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(json!.Employee.Name, Is.EqualTo("UpdatedName"));
        Assert.That(firstFromDb.Name, Is.Not.EqualTo("UpdatedName"));
        Assert.That(json!.Employee.Surname, Is.EqualTo("UpdatedSurname"));
        Assert.That(firstFromDb.Surname, Is.Not.EqualTo("UpdatedSurname"));
        Assert.That(json!.Employee.Email, Is.EqualTo("updatedEmail@email.com"));
        Assert.That(firstFromDb.Email.Value, Is.Not.EqualTo("updatedEmail@email.com"));
        var hashResult = passwordHasher.VerifyHashedPassword(firstFromDb, json.Employee.Password!, "tEST123!!!");
        Assert.That(hashResult, Is.EqualTo(PasswordVerificationResult.Success));
    }

    [Test]
    public async Task Update_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();
        var firstFromDb = dbContext.Employees.FirstOrDefault();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.PutAsJsonAsync($"employees", new EmployeeUpdateRequestDTO(firstFromDb!.Id, "", "", "", ""));
        var problemDetails = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
        using var dbContext2 = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployeesAfter = await dbContext2.Employees.ToListAsync();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
        Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        var errors = problemDetails.GetErrors();

        // Assert
        AssertExtensions.AssertError(errors, nameof(EmployeeUpdateRequestDTO.Name), "'Name' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeUpdateRequestDTO.Surname), "'Surname' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeUpdateRequestDTO.Email), "'Email' must not be empty.");
        AssertExtensions.AssertError(errors, nameof(EmployeeUpdateRequestDTO.Email), "'Email' is not a valid email address.");
    }
}