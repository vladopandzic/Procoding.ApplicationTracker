using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;
using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;


[TestFixture]
internal class UpdateEmployeeEndpointTests
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
    public async Task UpdateEmployee_ShouldUpdateEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Employees.FirstOrDefault();

        //Act
        var response = await client.PutAsJsonAsync($"employees",
                                                   new UpdateEmployeeCommand(firstFromDb!.Id, "UpdatedName", "UpdatedSurname", "updatedEmail@email.com"));
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
        var response = await client.PutAsJsonAsync($"employees", new EmployeeUpdateRequestDTO(firstFromDb!.Id, "", "", ""));
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