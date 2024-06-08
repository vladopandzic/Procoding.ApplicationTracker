using Bogus;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Candidates;

[TestFixture]
public class GetAllCandidatesEndpointTests : TestBase
{
    [Test]
    public async Task GetAllCandidates_ShouldSupportPagination()
    {
        //Arrange
        var client = _factory.CreateClient();
        using var dbContext = _factory.Services.GetRequiredScopedService<ApplicationDbContext>();
        var passwordHasher = _factory.Services.GetRequiredScopedService<IPasswordHasher<Candidate>>();
        await dbContext.AddRangeAsync(GenerateCandidates(50, passwordHasher));
        await dbContext.SaveChangesAsync();

        //Act
        await LoginHelper.LoginEmployee(client);
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

    [Test]
    public async Task GetAllCandidates_WhenLoggedInAsEmployee_ShouldReturnForbidden()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        await LoginHelper.LoginCandidate(client);
        var newRequest = new CandidateGetListRequestDTO()
        {
            PageNumber = 1,
            PageSize = 10
        };
        var response = await client.GetAsync($"candidates?{newRequest.ToQueryString()}");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
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