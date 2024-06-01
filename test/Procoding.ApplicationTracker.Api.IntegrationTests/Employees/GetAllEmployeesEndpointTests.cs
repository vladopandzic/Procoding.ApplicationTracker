using Bogus;
using Procoding.ApplicationTracker.Domain;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Employees;

[TestFixture]
public class GetAllEmployeesEndpointTests
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
    public async Task GetAllEmployees_ShouldReturnListOfEmployees()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetFromJsonAsync<EmployeeListResponseDTO>("employees");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employees.Count, Is.EqualTo(DatabaseSeedData.GetEmployees().Count));
    }

    [Test]
    public async Task GetAllEmployees_ShouldSupportPagination()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        await dbContext.AddRangeAsync(GenerateEmployees(50));
        await dbContext.SaveChangesAsync();

        //Act
        var newRequest = new EmployeeGetListRequestDTO()
        {
            PageNumber = 1,
            PageSize = 10
        };
        var response = await client.GetFromJsonAsync<EmployeeListResponseDTO>($"employees?{newRequest.ToQueryString()}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Employees.Count, Is.EqualTo(10));
    }

    public static List<Employee> GenerateEmployees(int count)
    {
        var faker = new Faker<Employee>().CustomInstantiator(f =>
        Employee.Create(id: Guid.NewGuid(),
                        name: f.Name.FirstName(),
                        surname: f.Name.LastName(),
                        email: new Email(f.Internet.Email())));


        return faker.Generate(count);
    }
}