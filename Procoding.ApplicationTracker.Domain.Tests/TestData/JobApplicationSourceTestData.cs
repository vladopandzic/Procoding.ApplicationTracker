using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData
{
    public static class JobApplicationSourceTestData
    {
        public static JobApplicationSource ValidJobApplicationSource => JobApplicationSource.Create(Guid.NewGuid(), "LinkedIn");

        public static JobApplicationSource InvalidJobApplicationSourceWithNullEmail => JobApplicationSource.Create(Guid.NewGuid(), null!);

        public static JobApplicationSource InvalidJobApplicationSourceWithEmptyEmail => JobApplicationSource.Create(Guid.NewGuid(), "");

        public static JobApplicationSource InvalidJobApplicationSourceWithToLongEmail => JobApplicationSource.Create(Guid.NewGuid(), new string('x', JobApplicationSource.MaxLengthForName + 1));

        public static JobApplicationSource InvalidJobApplicationSourceWithToShortEmail => JobApplicationSource.Create(Guid.NewGuid(), new string('x', JobApplicationSource.MinLengthForName - 1));

    }
}
