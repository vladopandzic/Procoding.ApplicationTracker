using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateInsertedResponseDTO
{
    public CandidateInsertedResponseDTO(CandidateDTO candidate)
    {
        Candidate = candidate;
    }

    public CandidateInsertedResponseDTO()
    {

    }
    public CandidateDTO Candidate { get; set; }
}
