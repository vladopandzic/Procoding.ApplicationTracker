using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData
{
    public static class JobApplicationTestData
    {
        public static JobApplication ValidJobApplication => JobApplication.Create(candidate: CandidateTestData.ValidCandidate,
                                                                                  id: Guid.NewGuid(),
                                                                                  jobApplicationSource: JobApplicationSourceTestData.ValidJobApplicationSource,
                                                                                  company: CompanyTestData.ValidCompany,
                                                                                  timeProvider: TimeProvider.System,
                                                                                  jobPositionTitle: "Senior .NET sw engineer",
                                                                                  jobAdLink: new Link("https://www.link2.com"),
                                                                                  workLocationType: WorkLocationType.Remote,
                                                                                  jobType: JobType.FullTime,
                                                                                  description: "desc");
    }
}
