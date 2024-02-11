using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Tests.TestData;

public static class CandidateTestData
{
    public static Candidate ValidCandidate => Candidate.Create(Guid.NewGuid(), "Vlado", "Pandžić", new ValueObjects.Email("pandzic.vlado@gmail.com"));

}

