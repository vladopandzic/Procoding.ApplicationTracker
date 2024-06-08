using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;
using Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests.WorkLocationTypes;


[TestFixture]
public class WorkLocationTypesEndpointTets : TestBase
{
    [Test]
    public async Task GetWorkLocationTypes_ShouldReturnListOfWorkLocationTypes()
    {
        //Arrange
        var client = _factory.CreateClient();
        var mapper = _factory.Services.GetRequiredService<IMapper>();

        //Act
        var response = await client.GetFromJsonAsync<WorkLocationTypeListResponse>("work-location-types");

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.WorkLocationTypes.Count, Is.EqualTo(WorkLocationType.GetAll().Count()));
    }
}
