using Bogus;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;

[TestFixture]
public class GetAllEmployeesEndpointTests : TestBase
{
    [Test]
    public async Task GetAllEmployees_ShouldReturnListOfEmployees()
    {
        //Arrange
        var client = _factory.CreateClient();
        var passwordHasher = _factory.Services.GetRequiredScopedService<IPasswordHasher<Employee>>();

        //Act
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<EmployeeListResponseDTO>("employees");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employees.Count, Is.EqualTo(DatabaseSeedData.GetEmployees(passwordHasher).Count));
    }

    [Test]
    public async Task GetAllEmployees_WhenLoggedInAsCandidate_ShouldReturnListOfEmployees()
    {
        //Arrange
        var client = _factory.CreateClient();
        var passwordHasher = _factory.Services.GetRequiredScopedService<IPasswordHasher<Employee>>();

        //Act
        await LoginHelper.LoginCandidate(client);
        var response = await client.GetAsync("employees");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task GetAllEmployees_ShouldSupportPagination()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var passwordHasher = _factory.Services.GetRequiredScopedService<IPasswordHasher<Employee>>();
        await dbContext.AddRangeAsync(GenerateEmployees(50, passwordHasher));
        await dbContext.SaveChangesAsync();

        //Act
        var newRequest = new EmployeeGetListRequestDTO()
        {
            PageNumber = 1,
            PageSize = 10
        };
        await LoginHelper.LoginEmployee(client);
        var response = await client.GetFromJsonAsync<EmployeeListResponseDTO>($"employees?{newRequest.ToQueryString()}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employees.Count, Is.EqualTo(10));
    }

    public static List<Employee> GenerateEmployees(int count, IPasswordHasher<Employee> passwordHasher)
    {
        var faker = new Faker<Employee>().CustomInstantiator(f => Employee.Create(id: Guid.NewGuid(),
                                                                                  name: f.Name.FirstName(),
                                                                                  surname: f.Name.LastName(),
                                                                                  email: new Email(f.Internet.Email()),
                                                                                  password: "test123",
                                                                                  passwordHasher));


        return faker.Generate(count);
    }
}