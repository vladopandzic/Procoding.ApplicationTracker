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
public class GetOneEmployeeEndpointTests
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
    public async Task GetOneEmployeeEndpointTests_ShouldReturnEmployee()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var firstFromDb = dbContext.Employees.FirstOrDefault();

        //Act
        var response = await client.GetFromJsonAsync<EmployeeResponseDTO>($"employees/{firstFromDb!.Id}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employee.Name, Is.EqualTo(firstFromDb.Name));
        Assert.That(response.Employee.Surname, Is.EqualTo(firstFromDb.Surname));
        Assert.That(response.Employee.Email, Is.EqualTo(firstFromDb.Email.Value));


    }
}
