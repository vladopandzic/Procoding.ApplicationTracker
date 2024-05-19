using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;


public static class DatabaseSeedData
{
    public static List<JobApplicationSource> GetJobApplicationSources()
    {
        return [JobApplicationSource.Create(Guid.Empty, "Linkedin")];
    }
}
