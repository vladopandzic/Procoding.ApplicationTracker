using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
public class GetAllCandidatesEndpointTests
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
    public async Task GetAllCandidates_ShouldReturnListOfCandidates()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetFromJsonAsync<CandidateListResponseDTO>("candidates");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Candidates.Count, Is.EqualTo(DatabaseSeedData.GetCandidates().Count));
    }

    [Test]
    public async Task GetAllCandidates_ShouldSupportPagination()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var passwordHasher = _factory.Services.GetRequiredService<IPasswordHasher<Candidate>>();
        await dbContext.AddRangeAsync(GenerateCandidates(50, passwordHasher));
        await dbContext.SaveChangesAsync();

        //Act
        var newRequest = new CandidateGetListRequestDTO()
        {
            PageNumber = 1,
            PageSize = 10
        };
        var response = await client.GetFromJsonAsync<CandidateListResponseDTO>($"candidates?{newRequest.ToQueryString()}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Candidates.Count, Is.EqualTo(10));
    }

    public static List<Candidate> GenerateCandidates(int count, IPasswordHasher<Candidate> passwordHasher)
    {
        var faker = new Faker<Candidate>().CustomInstantiator(f => Candidate.Create(id: Guid.NewGuid(),
                                                                                    name: f.Name.FirstName(),
                                                                                    surname: f.Name.LastName(),
                                                                                    email: new Email(f.Internet.Email()),
                                                                                    password: f.Internet.Password(),
                                                                                    passwordHasher));


        return faker.Generate(count);
    }
}