using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateUpdatedResponseDTO
{
    public CandidateUpdatedResponseDTO(CandidateDTO candidate)
    {
        Candidate = candidate;
    }

    public CandidateUpdatedResponseDTO()
    {
    }

    public CandidateDTO Candidate { get; set; }
}
