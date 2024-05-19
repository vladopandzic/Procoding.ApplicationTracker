using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.Companies;

[TestFixture]
public class GetAllCompaniesEndpointTests
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
            await _factory.DisposeAsync();
    }

    [Test]
    public async Task GetAllCompanies_ShouldReturnListOfCompanies()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetFromJsonAsync<JobApplicationSourceListResponseDTO>("job-application-sources");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.JobApplicationSources.Count, Is.EqualTo(DatabaseSeedData.GetJobApplicationSources().Count));
    }
}