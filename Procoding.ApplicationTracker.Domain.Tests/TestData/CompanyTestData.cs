using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData
{
    public static class CompanyTestData
    {
        public static Company ValidCompany => Company.Create(new ValueObjects.CompanyName("Procoding"), new ValueObjects.Link("https://procoding.com.hr"));

    }
}
