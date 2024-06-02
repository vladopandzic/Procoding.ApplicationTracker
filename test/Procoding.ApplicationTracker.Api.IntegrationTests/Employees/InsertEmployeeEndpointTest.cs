using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;

[TestFixture]
internal class InsertEmployeeEndpointTests
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
    public async Task InsertEmployee_ShouldInsertNewEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"employees", new EmployeeInsertRequestDTO("NameNew", "SurnameNew", "newemail@newemail.com", "pass123"));
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
    public async Task InsertEmployee_IfBadRequest_ShouldReturnBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var allEmployees = await dbContext.Employees.ToListAsync();

        //Act
        var response = await client.PostAsJsonAsync($"employees", new EmployeeInsertRequestDTO("", "", "",""));
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