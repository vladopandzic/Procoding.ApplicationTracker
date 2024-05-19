using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData
{
    public static class JobApplicationTestData
    {
        public static JobApplication ValidJobApplication => JobApplication.Create(candidate: CandidateTestData.ValidCandidate,
                                                                                 id: Guid.NewGuid(),
                                                                                 jobApplicationSource: JobApplicationSourceTestData.ValidJobApplicationSource,
                                                                                 company: CompanyTestData.ValidCompany,
                                                                                 timeProvider: TimeProvider.System);

    }
}
