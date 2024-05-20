using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        List<JobApplicationSource> jobApplicationSources = [JobApplicationSource.Create(Guid.Empty, "RemoteOk"),
                                                           JobApplicationSource.Create(Guid.Empty, "MojPosao"),
                                                           JobApplicationSource.Create(Guid.Empty, "ITJobsCroatia")];

        await dbContext.AddRangeAsync(jobApplicationSources);

        await dbContext.SaveChangesAsync();
    }
}
