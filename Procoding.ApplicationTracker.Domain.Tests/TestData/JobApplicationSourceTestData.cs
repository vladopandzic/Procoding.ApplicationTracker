using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData
{
    public static class JobApplicationSourceTestData
    {
        public static JobApplicationSource ValidJobApplicationSource => JobApplicationSource.Create(Guid.NewGuid(), "LinkedIn");

    }
}
