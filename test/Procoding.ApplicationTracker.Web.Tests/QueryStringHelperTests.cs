using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Procoding.ApplicationTracker.DTOs.Request.Base;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;

namespace Procoding.ApplicationTracker.Web.Tests;

[TestFixture]
public class QueryStringHelperTests
{
    [Test]
    public void ToQueryString_WithApiRequestModel_ReturnsCorrectQueryString()
    {
        // Arrange
        var apiRequest = new CandidateGetListRequestDTO
        {
            PageNumber = 1,
            PageSize = 10,
            Filters = new List<FilterModelDto>
            {
                new FilterModelDto { Key = "Name", Value = "John", Operator = "Equals" },
                new FilterModelDto { Key = "City", Value = "New York", Operator = "Contains" }
            }
        };

        // Act
        var queryString = apiRequest.ToQueryString();

        // Assert
        Assert.AreEqual("PageNumber=1&PageSize=10&Filters[0].Key=Name&Filters[0].Value=" +
            "John&Filters[0].Operator=Equals&Filters[1].Key=City&Filters[1].Value=" +
            "New%20York&Filters[1].Operator=Contains", queryString);
    }
}